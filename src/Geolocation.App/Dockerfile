FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV HOSTNAME "Geolocation"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Geolocation.App/Geolocation.App.csproj", "Geolocation.App/"]
COPY ["Geolocation.Domain/Geolocation.Domain.csproj", "Geolocation.Domain/"]
COPY ["Geolocation.Infrastructure/Geolocation.Infrastructure.csproj", "Geolocation.Infrastructure/"]
RUN dotnet restore "Geolocation.App/Geolocation.App.csproj"
COPY . .
WORKDIR "/src/Geolocation.App"
RUN dotnet build "Geolocation.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geolocation.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geolocation.App.dll"]