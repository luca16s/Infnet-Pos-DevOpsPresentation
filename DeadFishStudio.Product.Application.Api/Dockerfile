FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

LABEL maintainer="luca.gian16@outlook.com"
LABEL version="1.1"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj", "DeadFishStudio.Product.Application.Api/"]
COPY ["DeadFishStudio.Product.Domain.Model/DeadFishStudio.Product.Domain.Model.csproj", "DeadFishStudio.Product.Domain.Model/"]
COPY ["DeadFishStudio.Product.Infrastructure.Data/DeadFishStudio.Product.Infrastructure.Data.csproj", "DeadFishStudio.Product.Infrastructure.Data/"]
COPY ["DeadFishStudio.Product.Infrastructure.CrossCutting/DeadFishStudio.Product.Infrastructure.CrossCutting.csproj", "DeadFishStudio.Product.Infrastructure.CrossCutting/"]
COPY ["DeadFishStudio.Product.Domain.Service/DeadFishStudio.Product.Domain.Service.csproj", "DeadFishStudio.Product.Domain.Service/"]
COPY ["DeadFishStudio.InfnetDevOps.Shared/DeadFishStudio.InfnetDevOps.Shared.csproj", "DeadFishStudio.InfnetDevOps.Shared/"]
RUN dotnet restore "DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj"
COPY ./ ./
WORKDIR /src/
RUN dotnet build "DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.dll"]