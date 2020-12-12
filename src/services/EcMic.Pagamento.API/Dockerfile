#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/EcMic.Pagamento.API/EcMic.Pagamento.API.csproj", "src/services/EcMic.Pagamento.API/"]
RUN dotnet restore "src/services/EcMic.Pagamento.API/EcMic.Pagamento.API.csproj"
COPY . .
WORKDIR "/src/src/services/EcMic.Pagamento.API"
RUN dotnet build "EcMic.Pagamento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcMic.Pagamento.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcMic.Pagamento.API.dll"]