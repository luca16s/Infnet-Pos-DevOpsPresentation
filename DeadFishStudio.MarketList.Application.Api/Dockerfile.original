FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.MarketList.Application.Api/DeadFishStudio.MarketList.Application.Api.csproj", "DeadFishStudio.MarketList.Application.Api/"]
RUN dotnet restore "DeadFishStudio.MarketList.Application.Api/DeadFishStudio.MarketList.Application.Api.csproj"
COPY . .
WORKDIR "/src/DeadFishStudio.MarketList.Application.Api"
RUN dotnet build "DeadFishStudio.MarketList.Application.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DeadFishStudio.MarketList.Application.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeadFishStudio.MarketList.Application.Api.dll"]