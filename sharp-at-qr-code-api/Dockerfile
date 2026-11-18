FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

COPY sharp-at-qr-code-api/src/sharp-at-qr-code-api.csproj sharp-at-qr-code-api/src/
COPY sharp-at-qr-code-api-library/src/sharp-at-qr-code-api-library.csproj sharp-at-qr-code-api-library/src/
COPY sharp-at-qr-code-library/src/sharp-at-qr-code-library.csproj sharp-at-qr-code-library/src/
COPY sharp-at-qr-code/src/sharp-at-qr-code.csproj sharp-at-qr-code/src/
COPY sharp-at-generics/src/sharp-at-generics.csproj sharp-at-generics/src/

RUN dotnet restore sharp-at-qr-code-api/src/sharp-at-qr-code-api.csproj

COPY . .
RUN dotnet publish sharp-at-qr-code-api/src/sharp-at-qr-code-api.csproj \
    --configuration Release \
    --no-restore \
    --output /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
RUN apt-get update \
    && apt-get install --no-install-recommends -y curl libfontconfig1 \
    && rm -rf /var/lib/apt/lists/* \
    && touch /var/log/at-qr-code-api-access.log \
    && chown "$APP_UID:$APP_UID" /var/log/at-qr-code-api-access.log

WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
USER $APP_UID

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
    CMD curl --fail --silent --show-error --max-time 3 http://localhost:8080/HealthCheck || exit 1

ENTRYPOINT ["dotnet", "sharp-at-qr-code-api.dll"]
