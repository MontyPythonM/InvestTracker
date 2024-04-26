FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bootstrapper/InvestTracker.Bootstrapper/InvestTracker.Bootstrapper.csproj", "src/Bootstrapper/InvestTracker.Bootstrapper/"]
COPY ["src/Modules/Calculators/src/InvestTracker.Calculators.Api/InvestTracker.Calculators.Api.csproj", "src/Modules/Calculators/src/InvestTracker.Calculators.Api/"]
COPY ["src/Modules/Calculators/src/InvestTracker.Calculators.Core/InvestTracker.Calculators.Core.csproj", "src/Modules/Calculators/src/InvestTracker.Calculators.Core/"]
COPY ["src/Shared/InvestTracker.Shared.Infrastructure/InvestTracker.Shared.Infrastructure.csproj", "src/Shared/InvestTracker.Shared.Infrastructure/"]
COPY ["src/Shared/InvestTracker.Shared.Abstractions/InvestTracker.Shared.Abstractions.csproj", "src/Shared/InvestTracker.Shared.Abstractions/"]
COPY ["src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Api/InvestTracker.InvestmentStrategies.Api.csproj", "src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Api/"]
COPY ["src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Infrastructure/InvestTracker.InvestmentStrategies.Infrastructure.csproj", "src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Infrastructure/"]
COPY ["src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Application/InvestTracker.InvestmentStrategies.Application.csproj", "src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Application/"]
COPY ["src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Domain/InvestTracker.InvestmentStrategies.Domain.csproj", "src/Modules/InvestmentStrategies/src/InvestTracker.InvestmentStrategies.Domain/"]
COPY ["src/Modules/Notifications/src/InvestTracker.Notifications.Api/InvestTracker.Notifications.Api.csproj", "src/Modules/Notifications/src/InvestTracker.Notifications.Api/"]
COPY ["src/Modules/Notifications/src/InvestTracker.Notifications.Core/InvestTracker.Notifications.Core.csproj", "src/Modules/Notifications/src/InvestTracker.Notifications.Core/"]
COPY ["src/Modules/Notifications/src/InvestTracker.Notifications.Infrastructure/InvestTracker.Notifications.Infrastructure.csproj", "src/Modules/Notifications/src/InvestTracker.Notifications.Infrastructure/"]
COPY ["src/Modules/Offers/src/InvestTracker.Offers.Api/InvestTracker.Offers.Api.csproj", "src/Modules/Offers/src/InvestTracker.Offers.Api/"]
COPY ["src/Modules/Offers/src/InvestTracker.Offers.Core/InvestTracker.Offers.Core.csproj", "src/Modules/Offers/src/InvestTracker.Offers.Core/"]
COPY ["src/Modules/Users/src/InvestTracker.Users.Api/InvestTracker.Users.Api.csproj", "src/Modules/Users/src/InvestTracker.Users.Api/"]
COPY ["src/Modules/Users/src/InvestTracker.Users.Core/InvestTracker.Users.Core.csproj", "src/Modules/Users/src/InvestTracker.Users.Core/"]
RUN dotnet restore "src/Bootstrapper/InvestTracker.Bootstrapper/InvestTracker.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/InvestTracker.Bootstrapper"
RUN dotnet build "InvestTracker.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InvestTracker.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestTracker.Bootstrapper.dll"]
