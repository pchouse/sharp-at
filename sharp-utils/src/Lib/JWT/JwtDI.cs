/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace PChouse.Utils.JWT;

public static class JwtDI
{
    extension(IServiceCollection serviceCollection)
    {

        /// <summary>
        /// Add the token handler service, to generate JWT and parse from http request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="options"></param>
        public void AddToken(string key, Action<TokenOptions> options)
        {
            var tokenOptions = new TokenOptions();
            options.Invoke(tokenOptions);

            serviceCollection.AddScoped<IToken>(services =>
            {

                return new Token(
                    services.GetRequiredService<ILogger<Token>>(),
                    tokenOptions,
                    key
                );

            });
        }

        /// <summary>
        /// Add the Bearer authentication service
        /// </summary>
        /// <param name="bearerOptions"></param>
        /// <returns></returns>
        public JwtBearerOptions? AddJwtBearerAuthentication(BearerOptions bearerOptions)
        {

            if (string.IsNullOrWhiteSpace(bearerOptions.TokenKey))
            {
                var rand = new Random();
                bearerOptions.TokenKey = rand.Next(
                    int.Parse("".PadLeft(32, '1')), int.Parse("".PadLeft(32, '9'))
                ).ToString();
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role,
                IssuerSigningKey = new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(bearerOptions.TokenKey)
                ),
            };

            JwtBearerOptions? jwtBearerOptions = null;

            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                jwtBearerOptions = options;

                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = c =>
                    {
                        var reason = string.Empty;

                        return Task.Run(() =>
                        {
                            try
                            {
                                var contentType = c.Request.ContentType?.ToLower();
                                if (contentType?.Contains("json", StringComparison.CurrentCultureIgnoreCase) != false) return;

                                reason = "Media content type must be application/json";
                            }
                            catch (Exception e)
                            {
                                reason = e.Message;
                            }

                            var failure = new BearerAuthenticationFailure()
                            {
                                Reason = reason,
                            };

                            c.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                            c.HttpContext.Response.ContentType = "application/json";

                            c.HttpContext.Response.WriteAsync(
                                JsonSerializer.Serialize(failure)
                            ).GetAwaiter().GetResult();
                        });
                    },

                    OnChallenge = c =>
                    {
                        return Task.Run(() =>
                        {
                            var reason = string.Empty;

                            try
                            {
                                c.HandleResponse();

                                string? auth = c.HttpContext.Request.Headers[HeaderNames.Authorization];

                                if (c.AuthenticateFailure == null && (auth?.StartsWith("Bearer ") ?? false)) return;

                                reason = c.AuthenticateFailure?.Message ?? "Access not authorized, invalid or not existent.";

                            }
                            catch (Exception e)
                            {
                                reason = e.Message;
                            }

                            c.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            c.HttpContext.Response.ContentType = "application/json";

                            var failure = new BearerAuthenticationFailure
                            {
                                Reason = reason
                            };

                            c.HttpContext.Response.WriteAsync(
                                JsonSerializer.Serialize(failure)
                            ).GetAwaiter().GetResult();

                        });
                    },
                };
            });

            var authorizationBuilder = serviceCollection.AddAuthorizationBuilder();

            foreach (var role in bearerOptions.Roles)
            {
                authorizationBuilder.AddPolicy(
                    role,
                    policy => policy.RequireAssertion(ctx => ctx.User.HasClaim(ClaimTypes.Role, role))
                );
            }

            serviceCollection.AddSingleton<IAuthorizationHandler, RoleInterceptHandler>();

            return jwtBearerOptions;
        }
    }
}