FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet


COPY RabbitMqTester/RabbitMqTester.csproj /RabbitMqTester/
RUN dotnet restore /RabbitMqTester/RabbitMqTester.csproj

COPY RabbitMqTester /RabbitMqTester/

FROM dotnet AS build
RUN dotnet publish RabbitMqTester/RabbitMqTester.csproj -c Release --output /app/published-app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
WORKDIR /app
COPY --from=build /app/published-app /app
ENTRYPOINT [ "dotnet", "/app/RabbitMqTester.dll" ]