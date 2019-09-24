FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.InfnetDevOps.Presentation/DeadFishStudio.InfnetDevOps.Presentation.csproj", "DeadFishStudio.InfnetDevOps.Presentation/"]
COPY ["DeadFishStudio.Product.Domain.Model/DeadFishStudio.Product.Domain.Model.csproj", "DeadFishStudio.Product.Domain.Model/"]
COPY ["DeadFishStudio.InfnetDevOps.ApiConnectionFactory/DeadFishStudio.InfnetDevOps.ApiConnectionFactory.csproj", "DeadFishStudio.InfnetDevOps.ApiConnectionFactory/"]
COPY ["DeadFishStudio.MarketList.Infrastructure.Data/DeadFishStudio.MarketList.Infrastructure.Data.csproj", "DeadFishStudio.MarketList.Infrastructure.Data/"]
COPY ["DeadFishStudio.MarketList.Domain.Model/DeadFishStudio.MarketList.Domain.Model.csproj", "DeadFishStudio.MarketList.Domain.Model/"]
COPY ["DeadFishStudio.InfnetDevOps.Shared/DeadFishStudio.InfnetDevOps.Shared.csproj", "DeadFishStudio.InfnetDevOps.Shared/"]
COPY ["DeadFishStudio.Product.Infrastructure.Data/DeadFishStudio.Product.Infrastructure.Data.csproj", "DeadFishStudio.Product.Infrastructure.Data/"]
COPY ["DeadFishStudio.MarketList.Application.Api/DeadFishStudio.MarketList.Application.Api.csproj", "DeadFishStudio.MarketList.Application.Api/"]
COPY ["DeadFishStudio.MarketList.Infrastructure.CrossCutting/DeadFishStudio.MarketList.Infrastructure.CrossCutting.csproj", "DeadFishStudio.MarketList.Infrastructure.CrossCutting/"]
COPY ["DeadFishStudio.MarketList.Domain.Service/DeadFishStudio.MarketList.Domain.Service.csproj", "DeadFishStudio.MarketList.Domain.Service/"]
RUN dotnet restore "DeadFishStudio.InfnetDevOps.Presentation/DeadFishStudio.InfnetDevOps.Presentation.csproj"
COPY . .
WORKDIR "/src/DeadFishStudio.InfnetDevOps.Presentation"
RUN dotnet build "DeadFishStudio.InfnetDevOps.Presentation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DeadFishStudio.InfnetDevOps.Presentation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeadFishStudio.InfnetDevOps.Presentation.dll"]
