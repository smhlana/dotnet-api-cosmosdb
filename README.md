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
On the new page, type Azure Cosmos DB on the search bar and select it from the list that appears, then select Create on the next page.
![search and select](https://user-images.githubusercontent.com/11193045/112724333-7f720580-8f1b-11eb-8484-0152057c5c60.png)

Under the Basics tab, fill in the required settings:

**Subscription** – Select the Azure subscription you want to use for this account

**Resource Group** – Select the resource group or Create new with a unique name
**Account Name** – Enter a unique account name
**API** – Select Core SQL for this project

**Location** – Choose the location closest to you or your users

**Capacity Mode** – Select Provisioned throughput



You don’t have to configure the settings in the other tabs. Let’s use the default settings.

