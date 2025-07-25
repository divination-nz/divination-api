﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
ADD https://media.wizards.com/2025/downloads/MagicCompRules%2020250725.txt ./Resources/rules.txt
COPY ["Divination.csproj", "./"]
RUN dotnet restore "Divination.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Divination.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Divination.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Divination.dll"]
