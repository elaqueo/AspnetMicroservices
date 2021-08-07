FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY ["services/catalog/Catalog.Api/Catalog.Api.csproj", "services/catalog/Catalog.Api/"]
RUN dotnet restore "services/catalog/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/services/catalog/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Catalog.Api.dll" ]
