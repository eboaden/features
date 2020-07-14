FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as restore
WORKDIR /src

COPY RandomDataGenerator.sln .

COPY RandomDataGenerator.Api/RandomDataGenerator.Api.csproj RandomDataGenerator.Api/
RUN dotnet restore "RandomDataGenerator.Api/RandomDataGenerator.Api.csproj"
COPY RandomDataGenerator.Api.Tests/RandomDataGenerator.Api.Tests.csproj RandomDataGenerator.Api.Tests/
RUN dotnet restore "RandomDataGenerator.Api.Tests/RandomDataGenerator.Api.Tests.csproj"

FROM restore as build
WORKDIR /src

COPY RandomDataGenerator.Api RandomDataGenerator.Api/
COPY RandomDataGenerator.Api.Tests RandomDataGenerator.Api.Tests
RUN dotnet test "RandomDataGenerator.Api.Tests/RandomDataGenerator.Api.Tests.csproj" -c Release

RUN dotnet publish "RandomDataGenerator.Api/RandomDataGenerator.Api.csproj" -c Release -o /app/publish


FROM base
WORKDIR /app

COPY --from=build /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000/

RUN chown www-data:www-data -R .
USER 33
ENTRYPOINT [ "dotnet", "RandomDataGenerator.Api.dll" ]