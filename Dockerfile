# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file into the container
COPY ["MinimalApiDemo.csproj", "./"]

# Restore the dependencies
RUN dotnet restore "MinimalApiDemo.csproj"

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR "/src"
RUN dotnet build "MinimalApiDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiDemo.csproj" -c Release -o /app/publish

# Copy the build files to the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiDemo.dll"]
