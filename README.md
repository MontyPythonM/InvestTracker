# InvestTracker
## About application

**InvestTracker** is the financial assets monitor application built as **Modular Monolith**, written in .NET 8.0.
The application allows you to enter selected financial assets and track changes in their value over time. 

## Modules
Depending on the complexity of the module, different architectural styles are used,
including a simple **CRUD** approach, along with **CQRS**, **Clean Architecture** and **Domain-Driven Design**.

Modules are integrated with each other based on an **event-driven architecture**, using a local-contract approach.

The database used is **PostgreSQL** and **EF Core** as ORM.

### Investment Strategies
The main module of the application with business logic for creating and displaying information about financial assets.
Investor can divide his assets into _investment strategies_ containing _portfolios_ (e.g. offensive, long-term, etc.).

The following assets are currently available:
- **Cash** (with currency conversion),
- **EDO** treasury bond,
- **COI** treasury bond

Supported currencies: PLN, USD, PHP, SEK, KRW, DKK, EUR, CNY, BRL, CZK, BGN, JPY , HUF, NOK, CHF, GBP.

This module periodically downloads exchange rates and Polish CPI inflation from public APIs.

### Offers
A module dealing with the publication of _offers_ by advisors and undertaking _collaboration_ with investors.
Starting _collaboration_ allows investor to share his _investment strategy_ with an advisor who can execute actions on his behalf.
For example, to optimize an investor's financial _portfolio_.

### Users
The module responsible for the registration and login and management of application users by administrators.

### Calculators
A module containing calculators for estimating investment returns, such as government bond yields under certain assumptions.

### Notifications
The module responsible for sending notifications to users. 
The notifications use the **SignalR** library. 

A test client has been created in the form of a console application.

### Shared features
Shared project with cross-cutting classes and abstractions.
For example, it contains building blocks for DDD, database connection support or CQRS and EDA implementations.

## How to start the solution

Type the following command:
```
docker-compose up -d
```
It will start the required infrastructure using Docker in the background. 
Then, you can start the application under src/Bootstrapper/Confab.Bootstrapper/ using your favorite IDE or CLI.
```
cd .\src\Bootstrapper\InvestTracker.Bootstrapper
dotnet run
```
Once the application is up and running, the API documentation is available at ```http://localhost:5200/swagger/index.html```.