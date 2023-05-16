#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SpecialOffers.csproj"
WORKDIR "/src"
RUN dotnet build "SpecialOffers.csproj" -c Release -o /api/build

FROM build AS publish
WORKDIR "/src"
RUN dotnet publish "SpecialOffers.csproj" -c Release -o /api/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /api/publish ./api
ENTRYPOINT dotnet api/SpecialOffers.dll