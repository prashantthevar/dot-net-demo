# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MinimalApiDemo/MinimalApiDemo.csproj", "MinimalApiDemo/"]
RUN dotnet restore "MinimalApiDemo/MinimalApiDemo.csproj"
COPY . .
WORKDIR "/src/MinimalApiDemo"
RUN dotnet build "MinimalApiDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiDemo.csproj" -c Release -o /app/publish

# Copy the build files to the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiDemo.dll"]
