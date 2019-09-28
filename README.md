|Azure Boards|
|------------|
|[![Board Status](https://dev.azure.com/DeadFishStudio/6c2f9ae8-4e0c-4332-94c8-686b930c05e3/2a3c83c7-fe34-4623-a133-3242e12a4586/_apis/work/boardbadge/bb939a76-4bfd-42d5-991d-53aacfb5df4f?columnOptions=1)](https://dev.azure.com/DeadFishStudio/6c2f9ae8-4e0c-4332-94c8-686b930c05e3/_boards/board/t/2a3c83c7-fe34-4623-a133-3242e12a4586/Microsoft.RequirementCategory/)|

|CodeFactor|CodeCov|TravisCI|ASP.NET Core CI|
|----------|-------|--------|---------------|
|[![CodeFactor](https://www.codefactor.io/repository/github/luca16s/infnet-pos-devopspresentation/badge)](https://www.codefactor.io/repository/github/luca16s/infnet-pos-devopspresentation)|[![codecov](https://codecov.io/gh/luca16s/Infnet-Pos-DevOpsPresentation/branch/master/graph/badge.svg)](https://codecov.io/gh/luca16s/Infnet-Pos-DevOpsPresentation)|[![Build Status](https://travis-ci.org/luca16s/Infnet-Pos-DevOpsPresentation.svg?branch=master)](https://travis-ci.org/luca16s/Infnet-Pos-DevOpsPresentation)|![](https://www.github.com/luca16s/Infnet-Pos-DevOpsPresentation/workflows/ASP.NET%20Core%20CI/badge.svg)|

Compose:
```
version: '3.4'

services:
  mssql:
    image: "microsoft/mssql-server-linux:latest"
    environment:
      SA_PASSWORD: "Alaska2017"
      ACCEPT_EULA: "Y"
    ports:
      - "5433:1433"

  deadfishstudio.infnetdevops.presentation:
    image: luca16s/devops_marketlist_presentation
    environment:
      - ProductUrl=http://product.api:80
      - MarketListUrl=http://marketlist.api:80/
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:80
    ports:
      - "53937:80"
    depends_on:
      - marketlist.api
      - product.api

  marketlist.api:
    image: luca16s/devops_marketlist_api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:80
    expose:
      - "80"
    ports:
      - "53452:80"
    depends_on:
      - mssql

  product.api:
    image: luca16s/devops_product_api
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:80
    expose:
      - "80"
    ports:
      - "65400:80"
    depends_on:
      - mssql
```


# Infnet-Pos-DevOpsPresentation
Repositório com referência ao respositório https://github.com/luca16s/SupermarketBudgetProject.

Repositório recriado para utilizar melhores práticas de desenvolvimento e ter integração com o Azure DevOps.
