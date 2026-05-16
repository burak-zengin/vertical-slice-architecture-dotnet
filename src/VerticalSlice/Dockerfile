# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Centralized MSBuild props are required by Central Package Management.
COPY ["global.json", "Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["src/VerticalSlice/VerticalSlice.csproj", "src/VerticalSlice/"]
RUN dotnet restore "./src/VerticalSlice/VerticalSlice.csproj"

COPY . .
WORKDIR "/src/src/VerticalSlice"
RUN dotnet build "./VerticalSlice.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VerticalSlice.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VerticalSlice.dll"]
