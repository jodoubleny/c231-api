#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["c231-qrder-api/c231-qrder-api.csproj", "c231-qrder-api/"]
RUN dotnet restore "c231-qrder-api/c231-qrder-api.csproj"
COPY . .
WORKDIR "/src/c231-qrder-api"
RUN dotnet build "c231-qrder-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "c231-qrder-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "c231-qrder-api.dll"]