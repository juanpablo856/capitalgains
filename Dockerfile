FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# copy full solution over
COPY . .
RUN dotnet build
# Restore as distinct layers
RUN dotnet restore

FROM build-env AS testrunner
WORKDIR /CapitalGains.Test
CMD ["dotnet", "test", "--logger:trx"]

# run the unit tests
FROM build AS test
WORKDIR /CapitalGains.Test
RUN dotnet test --logger:trx

# Build and publish a release
FROM build AS publish
WORKDIR /CapitalGains
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet", "CapitalGains.dll"]

