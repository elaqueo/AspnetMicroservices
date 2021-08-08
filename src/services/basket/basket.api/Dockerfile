FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY ["services/basket/basket.api/basket.api.csproj", "services/basket/basket.api/"]
RUN dotnet restore "services/basket/basket.api/basket.api.csproj"
COPY . .
WORKDIR "/src/services/basket/basket.api"
RUN dotnet build "basket.api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "basket.api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "basket.api.dll" ]
