#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Presentations/DynamicConfiguration.WEB/DynamicConfiguration.WEB.csproj", "src/Presentations/DynamicConfiguration.WEB/"]
RUN dotnet restore "src/Presentations/DynamicConfiguration.WEB/DynamicConfiguration.WEB.csproj"
COPY . .
WORKDIR "/src/src/Presentations/DynamicConfiguration.WEB"
RUN dotnet build "DynamicConfiguration.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamicConfiguration.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicConfiguration.WEB.dll"]