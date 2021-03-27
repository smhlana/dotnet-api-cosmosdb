# dotnet-api-cosmosdb
Connect to an Azure Cosmos DB account from a .NET Core Web API using C#

## Introduction
Non-relational databases, also known as NoSQL databases, provide flexible schemas and scale easily with large amounts of data. NoSQL databases don’t store data in relational tables, instead nesting related data within a single structure or document. Azure Cosmos DB is a fully-managed NoSQL database.

This article demonstrates how to configure and use an Azure Cosmos DB SQL account. We will create and query Cosmos DB resources from a .NET Core API using C# and the Microsoft.Azure.Cosmos library running on .NET 5.0. We also use Postman to test the API. In our example, we create a database to store information about cities, add a city to the database, and query that information. 

## Prerequisites
For this project, you need:
  - Visual Studio 2019. Download and install Visual Studio 2019 Community Edition. 
  - .NET 5.0 SDK. Download it from Microsoft  
  - Postman to test the web API. Download it from Postman. 
  - An active Microsoft Azure account. If you don’t have one already, set one up on the Microsoft website.

## Create an Azure Cosmos DB Account
To create an Azure Cosmos DB account, first ensure you have an active Microsoft Azure account. Then, log on to the Azure portal.

Next, from the menu or Azure Services, select Create a resource.
![Create resource](https://user-images.githubusercontent.com/11193045/112723983-d5de4480-8f19-11eb-9512-09f7f530a609.PNG)
