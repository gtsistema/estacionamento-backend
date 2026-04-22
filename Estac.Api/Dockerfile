FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Estac.Api/Estac.Api.csproj", "Estac.Api/"]
COPY ["Estac.Domain/Estac.Domain.csproj", "Estac.Domain/"]
COPY ["Estac.Infra/Estac.Infra.csproj", "Estac.Infra/"]
COPY ["Estac.CrossCutting/Estac.CrossCutting.csproj", "Estac.CrossCutting/"]
COPY ["Estac.Service/Estac.Service.csproj", "Estac.Service/"]

RUN dotnet restore "Estac.Api/Estac.Api.csproj"

COPY . .

WORKDIR "/src/Estac.Api"
RUN dotnet publish "Estac.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Estac.Api.dll"]