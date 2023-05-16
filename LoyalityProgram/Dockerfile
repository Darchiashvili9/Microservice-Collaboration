#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "LoyaltyProgram/LoyaltyProgram.csproj"
RUN dotnet restore "EventConsumer/EventConsumer.csproj"
WORKDIR "/src/LoyaltyProgram"
RUN dotnet build "LoyaltyProgram.csproj" -c Release -o /api/build
WORKDIR "/src/EventConsumer"
RUN dotnet build "EventConsumer.csproj" -c Release -o /consumer/build/consumer

FROM build AS publish
WORKDIR "/src/LoyaltyProgram"
RUN dotnet publish "LoyaltyProgram.csproj" -c Release -o /api/publish
WORKDIR "/src/EventConsumer"
RUN dotnet publish "EventConsumer.csproj" -c Release -o /consumer/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /api/publish ./api
COPY --from=publish /consumer/publish ./consumer
ENTRYPOINT dotnet $STARTUPDLL