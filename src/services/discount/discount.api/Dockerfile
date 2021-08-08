FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY ["services/discount/discount.api/discount.api.csproj", "services/discount/discount.api/"]
RUN dotnet restore "services/discount/discount.api/discount.api.csproj"
COPY . .
WORKDIR "/src/services/discount/discount.api"
RUN dotnet build "discount.api.csproj" -c Release -o /app/build

FROM build as publish
RUN dotnet publish "discount.api.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "discount.api.dll" ]
