cd ../DeadFishStudio.MarketList.Domain.Model.Tests/
dotnet restore
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='../DeadFishStudioMarketListDomainModel.xml'