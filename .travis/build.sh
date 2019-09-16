#!/usr/bin/env bash

( cd ../DeadFishStudio.Product.Domain.Model/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.Data/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Domain.Service/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.CrossCutting/ && dotnet restore && dotnet build )

( cd ../DeadFishStudio.Product.Application.Api/  && dotnet restore && dotnet build )