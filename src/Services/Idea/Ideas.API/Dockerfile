#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Ideas.API/Ideas.API.csproj", "Ideas.API/"]
COPY ["Ideas.Infrastructure/Ideas.Infrastructure.csproj", "Ideas.Infrastructure/"]
COPY ["Ideas.Domain/Ideas.Domain.csproj", "Ideas.Domain/"]
RUN dotnet restore "Ideas.API/Ideas.API.csproj"
COPY . .
WORKDIR "/src/Ideas.API"
RUN dotnet build "Ideas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ideas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ideas.API.dll"]