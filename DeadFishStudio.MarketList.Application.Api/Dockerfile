FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.MarketList.Application.Api/DeadFishStudio.MarketList.Application.Api.csproj", "DeadFishStudio.MarketList.Application.Api/"]
COPY ["DeadFishStudio.MarketList.Infrastructure.CrossCutting/DeadFishStudio.MarketList.Infrastructure.CrossCutting.csproj", "DeadFishStudio.MarketList.Infrastructure.CrossCutting/"]
COPY ["DeadFishStudio.MarketList.Infrastructure.Data/DeadFishStudio.MarketList.Infrastructure.Data.csproj", "DeadFishStudio.MarketList.Infrastructure.Data/"]
COPY ["DeadFishStudio.MarketList.Domain.Model/DeadFishStudio.MarketList.Domain.Model.csproj", "DeadFishStudio.MarketList.Domain.Model/"]
COPY ["DeadFishStudio.InfnetDevOps.Shared/DeadFishStudio.InfnetDevOps.Shared.csproj", "DeadFishStudio.InfnetDevOps.Shared/"]
COPY ["DeadFishStudio.MarketList.Domain.Service/DeadFishStudio.MarketList.Domain.Service.csproj", "DeadFishStudio.MarketList.Domain.Service/"]
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
