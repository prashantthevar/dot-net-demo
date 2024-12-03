# Use the .NET 6 runtime for the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET 6 SDK for the build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project files into the container
COPY ./src

# Run restore
RUN dotnet restore

# Add a diagnostic step to list files in the container
RUN ls -la /src

# Publish the app
RUN dotnet publish -c Release -o /app

# Use the runtime image for the final image
FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "MinimalApiDemo.dll"]
