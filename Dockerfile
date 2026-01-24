# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files
COPY src/acciovac.API/acciovac.API.csproj acciovac.API/
COPY src/acciovac.Application/acciovac.Application.csproj acciovac.Application/
COPY src/acciovac.Domain/acciovac.Domain.csproj acciovac.Domain/
COPY src/acciovac.Infrastructure/acciovac.Infrastructure.csproj acciovac.Infrastructure/

# Restore
RUN dotnet restore acciovac.API/acciovac.API.csproj

# Copy everything else
COPY src/ .

# Build
WORKDIR /src/acciovac.API
RUN dotnet build acciovac.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish acciovac.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "acciovac.API.dll"]
