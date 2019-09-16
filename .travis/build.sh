#!/usr/bin/env bash
( cd ../DeadFishStudio.Product.Domain.Model/
&& dotnet tool install --global coverlet.console
&& dotnet restore
&& dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.Data/
&& dotnet tool install --global coverlet.console
&& dotnet restore
&& dotnet build )

( cd ../DeadFishStudio.Product.Domain.Service/
&& dotnet tool install --global coverlet.console
&& dotnet restore
&& dotnet build )

( cd ../DeadFishStudio.Product.Infrastructure.CrossCutting/
&& dotnet tool install --global coverlet.console
&& dotnet restore
&& dotnet build )

(cd ../DeadFishStudio.Product.Application.Api/
&& dotnet tool install --global coverlet.console
&& dotnet restore
&& dotnet build )