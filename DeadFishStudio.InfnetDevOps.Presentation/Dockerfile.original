FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeadFishStudio.InfnetDevOps.Presentation/DeadFishStudio.InfnetDevOps.Presentation.csproj", "DeadFishStudio.InfnetDevOps.Presentation/"]
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