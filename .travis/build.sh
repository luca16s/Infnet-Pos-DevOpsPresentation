#!/usr/bin/env bash

( cd ../DeadFishStudio.Product.Domain.Model/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.Data/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Domain.Service/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.CrossCutting/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.InfnetDevOps.Shared/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Application.Api/  && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Domain.Model/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Domain.Service/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Infrastructure.Data/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Infrastructure.CrossCutting/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Application.Api/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.MarketList.Domain.Model.Tests/ && dotnet restore && dotnet build )