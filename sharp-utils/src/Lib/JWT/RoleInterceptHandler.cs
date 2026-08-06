/**
* MIT License  
* Copyright(c) 2026 Reflexão, Estudos e Sistemas Informáticos, LDA
* @Author João M F Rebelo 
*/

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PChouse.Utils.JWT;

/// <summary>
/// Authorize the roles
/// </summary>
public class RoleInterceptHandler : AuthorizationHandler<RolesAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RolesAuthorizationRequirement requirement)
    {
        return Task.Run(() =>
        {
            var requireRoles = context.Requirements.ToList()
                .OfType<RolesAuthorizationRequirement>()
                .SelectMany(r => r.AllowedRoles)
                .ToList();


            var userRolesToken = context.User.Claims.ToList()
                .Where(claim => claim.Type == ClaimTypes.Role)
                .Select(claim => claim.Value)
                .FirstOrDefault();

            var userRoles = new List<string>();

            if (userRolesToken?.StartsWith('[') == true && userRolesToken.EndsWith(']'))
            {
                userRoles.AddRange(
                    System.Text.Json.JsonSerializer.Deserialize<List<string>>(userRolesToken) ?? []
                );
            }
            else if (!string.IsNullOrEmpty(userRolesToken))
            {
                userRoles.Add(userRolesToken);
            }

            if (userRoles.Intersect(requireRoles).Any())
            {
                context.Succeed(requirement);
            }
        });
    }
}