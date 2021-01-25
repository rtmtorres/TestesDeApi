FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG SONARQUBE_TOKEN
ARG SONARQUBE_PROJECT
ENV SONARQUBE_TOKEN $SONARQUBE_TOKEN
# ARG SONARQUBE_PROJECT
ENV SONARQUBE_PROJECT $SONARQUBE_PROJECT

RUN apt-get update && apt-get install default-jre -y --no-install-recommends
# RUN dotnet tool install --global dotnet-sonarscanner 
RUN dotnet tool install dotnet-reportgenerator-globaltool --tool-path /dotnetglobaltools
RUN dotnet tool install --global dotnet-sonarscanner --version 4.10.0
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /src

COPY RT.sln .
COPY docker-compose.dcproj .
COPY ["src/RT.Api/RT.Api.csproj", "src/RT.Api/"]
COPY ["src/RT.Application/RT.Application.csproj", "src/RT.Application/"]
COPY ["src/RT.Domain/RT.Domain.csproj", "src/RT.Domain/"]
COPY ["src/RT.Infra.Dapper/RT.Infra.Dapper.csproj", "src/RT.Infra.Dapper/"]
COPY ["src/RT.Infra.Ef/RT.Infra.Ef.csproj", "src/RT.Infra.Ef/"]
COPY ["tests/RT.UnitTests/RT.UnitTests.csproj", "tests/RT.UnitTests/"]
RUN dotnet restore "RT.sln"
COPY . .
WORKDIR /src/src/RT.Api
RUN dotnet build "RT.Api.csproj" -c Release -o /app/build


FROM build as testrunner
WORKDIR /src
RUN dotnet test tests/RT.UnitTests/RT.UnitTests.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
RUN dotnet build-server shutdown
RUN dotnet sonarscanner begin /k:${SONARQUBE_PROJECT} /d:sonar.host.url="http://host.docker.internal:9000" /d:sonar.login=${SONARQUBE_TOKEN} /d:sonar.cs.opencover.reportsPaths="tests/RT.UnitTests/coverage.opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs"
RUN dotnet build /src/src/RT.Api/RT.Api.csproj
RUN dotnet sonarscanner end /d:sonar.login=${SONARQUBE_TOKEN}
RUN /dotnetglobaltools/reportgenerator "-reports:tests/RT.UnitTests/coverage.opencover.xml" "-targetdir:/out/testresults" "-reporttypes:HTMLInline;HTMLChart"


FROM build AS publish
RUN dotnet publish "RT.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RT.Api.dll"]
