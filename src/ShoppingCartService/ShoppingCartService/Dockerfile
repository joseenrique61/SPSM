﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShoppingCartService/ShoppingCartService.csproj", "ShoppingCartService/"]
RUN dotnet restore "ShoppingCartService/ShoppingCartService.csproj"
COPY . .
WORKDIR "/src/ShoppingCartService"
RUN dotnet build "ShoppingCartService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShoppingCartService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCartService.dll"]
