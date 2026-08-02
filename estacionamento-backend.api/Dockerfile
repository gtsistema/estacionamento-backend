FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["estacionamento-backend.api/estacionamento-backend.api.csproj", "estacionamento-backend.api/"]
COPY ["estacionamento-backend.domain/estacionamento-backend.domain.csproj", "estacionamento-backend.domain/"]
COPY ["estacionamento-backend.infra/estacionamento-backend.infra.csproj", "estacionamento-backend.infra/"]
COPY ["estacionamento-backend.crosscutting/estacionamento-backend.crosscutting.csproj", "estacionamento-backend.crosscutting/"]
COPY ["estacionamento-backend.service/estacionamento-backend.service.csproj", "estacionamento-backend.service/"]

RUN dotnet restore "estacionamento-backend.api/estacionamento-backend.api.csproj"

COPY . .

WORKDIR "/src/estacionamento-backend.api"
RUN dotnet publish "estacionamento-backend.api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
ENV PathBase=/estac
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "estacionamento-backend.api.dll"]
