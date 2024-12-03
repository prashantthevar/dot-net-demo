# Use .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set working directory
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application and publish it
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Use .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "MinimalApiDemo.dll"]
