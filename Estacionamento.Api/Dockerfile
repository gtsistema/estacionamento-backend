FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Estacionamento.Api/Estacionamento.Api.csproj", "Estacionamento.Api/"]
COPY ["Estacionamento.Domain/Estacionamento.Domain.csproj", "Estacionamento.Domain/"]
COPY ["Estacionamento.Infra/Estacionamento.Infra.csproj", "Estacionamento.Infra/"]
COPY ["Estacionamento.CrossCutting/Estacionamento.CrossCutting.csproj", "Estacionamento.CrossCutting/"]
COPY ["Estacionamento.Service/Estacionamento.Service.csproj", "Estacionamento.Service/"]

RUN dotnet restore "Estacionamento.Api/Estacionamento.Api.csproj"

COPY . .

WORKDIR "/src/Estacionamento.Api"
RUN dotnet publish "Estacionamento.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Estacionamento.Api.dll"]