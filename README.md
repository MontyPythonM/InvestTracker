# InvestTracker

## About application

**InvestTracker** is the financial assets monitor application built as **Modular Monolith**, written in .NET 7.0.

Depending on the complexity of the module, different architectural styles are used, 
including a simple **CRUD** approach, along with **CQRS**, 
**Clean Architecture** and **Domain-Driven Design**.

Modules are integrated with each other based on an **event-driven architecture**, using a local-contract approach.

The database used is **PostgreSQL** and **EF Core** as ORM.

## Modules

### Investment Strategies


### Offers

### Users

### Calculators

### Notifications

## Shared features

## Bootstrapper

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

## How to use application