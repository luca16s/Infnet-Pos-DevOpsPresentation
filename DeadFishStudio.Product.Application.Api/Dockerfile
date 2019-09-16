FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

MAINTAINER Gian Luca da Silva Figueiredo luca.gian16@outlook.com

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj", "DeadFishStudio.Product.Application.Api/"]
RUN dotnet restore "DeadFishStudio.Product.Application.Api/DeadFishStudio.Product.Application.Api.csproj"
COPY . .
WORKDIR "/src/DeadFishStudio.Product.Application.Api"
RUN dotnet build "DeadFishStudio.Product.Application.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DeadFishStudio.Product.Application.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeadFishStudio.Product.Application.Api.dll"]