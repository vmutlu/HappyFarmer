FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build 
WORKDIR /app

COPY ./HappyFarmer.Entities/*.csproj ./HappyFarmer.Entities/
COPY ./HappyFarmer.DataAccess/*.csproj ./HappyFarmer.DataAccess/
COPY ./HappyFarmer.Business/*csproj ./HappyFarmer.Business/
COPY ./HappyFarmer.Configuration/*csproj ./HappyFarmer.Configuration/
COPY ./HappyFarmer.UI/*csproj ./HappyFarmer.UI/
COPY ./HappyFarmer.API/*csproj ./HappyFarmer.API/
COPY *.sln .

RUN dotnet restore
COPY . .

RUN dotnet publish ./HappyFarmer.UI/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build /publish .

ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet","HappyFarmer.UI.dll"]