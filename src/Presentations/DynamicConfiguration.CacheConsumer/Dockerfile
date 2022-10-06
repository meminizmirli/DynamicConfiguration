#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Presentations/DynamicConfiguration.CacheConsumer/DynamicConfiguration.CacheConsumer.csproj", "src/Presentations/DynamicConfiguration.CacheConsumer/"]
RUN dotnet restore "src/Presentations/DynamicConfiguration.CacheConsumer/DynamicConfiguration.CacheConsumer.csproj"
COPY . .
WORKDIR "/src/src/Presentations/DynamicConfiguration.CacheConsumer"
RUN dotnet build "DynamicConfiguration.CacheConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamicConfiguration.CacheConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicConfiguration.CacheConsumer.dll"]