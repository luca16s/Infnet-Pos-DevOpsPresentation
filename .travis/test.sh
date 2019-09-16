cd ../DeadFishStudio.Product.Domain.Model.UnitTest/
dotnet restore
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput='../DeadFishStudioProductDomainModel.xml'