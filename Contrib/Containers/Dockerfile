FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /src
COPY ["WalletAnonTx.Backend/WalletAnonTx.Backend.csproj", "WalletAnonTx.Backend/"]
COPY ["WalletAnonTx/WalletAnonTx.csproj", "WalletAnonTx/"]
RUN dotnet restore "WalletAnonTx.Backend/WalletAnonTx.Backend.csproj"
COPY . .
WORKDIR "/src/WalletAnonTx.Backend"
RUN dotnet build "WalletAnonTx.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WalletAnonTx.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WalletAnonTx.Backend.dll"]
