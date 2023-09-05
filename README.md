# Shop Web-API  

## Summary
This solution is a **ASP.NET Web-API** project for managing simple Shop system.  
Here you can manage your **Customers** and **Orders**.  
This project is also connected to **Azure Service Bus**, so other applications can be notified on new **Order** creation.  

## How to run the application  

### Using Docker  
To run the application using the Docker you have to follow these simple steps:  
- Open `docker-compose.yaml` file in eny IDE
- Here environment variables are need to set:
  - Find text `- ASPNETCORE_ENVIRONMENT=<environment>` and change `<environment>` to needed **Environment** name. For example, `Development`  
  - Find text `Azure:ServiceBus:ConnectionString=<azure_service_bus_connection_string>` and change `<azure_service_bus_connection_string>` to connection string for **Azure Service Bus**, that will receive a message about **Order** creation  
  - Find text `Azure:ServiceBus:QueueOrTopicName=<azure_service_bus_queue_or_topic>` and change `<azure_service_bus_queue_or_topic>` to name of **Azure Service Bus Queue** or **Topic** that will receive a message about **Order** creation  
- Save the file and open solution directory in the terminal  
- Run command `docker-compose up`  
- Wait =)  

The application is run by docker using port **3000 for https** url and **3001 for http**.  
If you want to remove all related **Docker Containers** you should open solution directory in terminal and run command `docker-compose down`. If you additionally want to remove all docker data related to this application run following commands:  
- `docker rmi shop-api`  
- `docker volume remove shop-backend_sql-server-volume`  

### Manually  
To run the application without Docker follow these steps:
- Ensure you have **.NET 6.0** installed on your machine  
- Ensure you have the **Microsoft SQL Server** database run and ready to use  
- *(Optional)* Ensure you have Azure Service Bus initialized and ready to use
- Open solution directory in the terminal
- Set environment variables by running these commands in terminal:
  - `dotnet user-secrets set "ConnectionStrings:Sql" "<database_connection_string>" --project Shop.Api`, where `<database_connection_string>` is connection string to your **Microsoft SQL Server** database  
  - `dotnet user-secrets set "Azure:ServiceBus:ConnectionString" "<azure_service_bus_connection_string>" --project Shop.Api`, where `<azure_service_bus_connection_string>` is the connection string for **Azure Service Bus**, that will receive a message about **Order** creation  
  - `dotnet user-secrets set "Azure:ServiceBus:QueueOrTopicName" "<azure_service_bus_queue_topic>" --project Shop.Api`, where `<azure_service_bus_queue_topic>` is the name of **Azure Service Bus Queue** or **Topic** that will receive a message about **Order** creation  
- Run command in terminal `dotnet restore`  
- Run command in terminal `dotnet build`  
- Run command in terminal `dotnet run --project Shop.Api`  

## Technologies and Tech-Decisions  
Here is the list of technologies and services used in this solution:
- .NET 6.0
- Microsoft SQL Server
- Entity Framework Core
- xUnit
- Azure Service Bus
- Docker

Solution contains 5 projects:
- **Shop.Api** - Startup Web-API project is responsible for communicating with application using HTTP requests  
- **Shop.Application** - Business-Layer project. Contains Services for processing data, received from Shop.Api project. Also contains contracts for Infrastructure layer.  
- **Shop.Infrastructure** - Infrastructure-Layer project. Contains implementation of DataAccess classes and 3d party services.  
- **Shop.Domain** - Domain-Layer project. Contains main entities and domain-logic related to finance calculations.  
- **Shop.Tests** - Test-Layer project. Contains Unit-Tests for Business-Layer methods.  

Schema of Project's references:  
```
Shop.Api         Shop.Tests
    |                |
   \|/              \|/
  +--------------------+
  |                    |
 \|/                  \|/
Shop.Application <-- Shop.Infrastructure
  |
 \|/
Shop.Domain
```  

## Testing
**Shop.Tests** Project of solution contains unit-tests for Business-Layer Project.  
For testing is used technology **xUnit**, and while testing the **In-Memory database** is used.  
To run tests open solution directory in terminal and run command `dotnet test`.  