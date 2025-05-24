# Usa la imagen oficial de .NET como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usa la imagen oficial de SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["gestion-personas-backend.csproj", "./"]
RUN dotnet restore "gestion-personas-backend.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "gestion-personas-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gestion-personas-backend.csproj" -c Release -o /app/publish

# Genera la imagen final para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gestion-personas-backend.dll"]
