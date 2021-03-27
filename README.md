# dotnet-api-cosmosdb
Connect to an Azure Cosmos DB account from a .NET Core Web API using C#

## Introduction
Non-relational databases, also known as NoSQL databases, provide flexible schemas and scale easily with large amounts of data. NoSQL databases don’t store data in relational tables, instead nesting related data within a single structure or document. Azure Cosmos DB is a fully-managed NoSQL database.

This article demonstrates how to configure and use an Azure Cosmos DB SQL account. We will create and query Cosmos DB resources from a .NET Core API using C# and the Microsoft.Azure.Cosmos library running on .NET 5.0. We also use Postman to test the API. In our example, we create a database to store information about cities, add a city to the database, and query that information. 

## Prerequisites
For this project, you need:
  - Visual Studio 2019. Download and install Visual Studio 2019 Community Edition.
    https://visualstudio.microsoft.com/downloads/
  - .NET 5.0 SDK. Download it from Microsoft  
    https://dotnet.microsoft.com/download/dotnet/5.0
  - Postman to test the web API. Download it from Postman. 
    https://www.postman.com/downloads/
  - An active Microsoft Azure account. If you don’t have one already, set one up on the Microsoft website.
    https://azure.microsoft.com/en-ca/free/

## Create an Azure Cosmos DB Account
To create an Azure Cosmos DB account, first ensure you have an active Microsoft Azure account. Then, log on to the Azure portal.

Next, from the menu or Azure Services, select **Create a resource**.

![Create resource](https://user-images.githubusercontent.com/11193045/112723983-d5de4480-8f19-11eb-9512-09f7f530a609.PNG)

On the new page, type **Azure Cosmos DB** on the search bar and select it from the list that appears, then select **Create** on the next page.

![search and select](https://user-images.githubusercontent.com/11193045/112724333-7f720580-8f1b-11eb-8484-0152057c5c60.png)

Under the **Basics** tab, fill in the required settings:

- **Subscription** – Select the Azure subscription you want to use for this account
- **Resource Group** – Select the resource group or Create new with a unique name
- **Account Name** – Enter a unique account name
- **API** – Select Core SQL for this project
- **Location** – Choose the location closest to you or your users
- **Capacity Mode** – Select Provisioned throughput

You don’t have to configure the settings in the other tabs. Let’s use the default settings.

![Basic settings](https://user-images.githubusercontent.com/11193045/112724691-4175e100-8f1d-11eb-852c-1191b0c703f1.PNG)

Click on **Review + create**. Review the settings when the page has finished loading and click **Create**. Then, wait for the deployment to complete. This can take several minutes.

![image](https://user-images.githubusercontent.com/11193045/112724548-9402cd80-8f1c-11eb-904b-5dec29f0644e.png)

When this is done, select **Go to resource**.

![image](https://user-images.githubusercontent.com/11193045/112724726-60747300-8f1d-11eb-810f-6bd3c564cea3.png)

## Create a Database and Container
After clicking on **Go to resource**, you will be taken to the Azure Cosmos DB account page. On the left menu, select **Data Explorer**, then select **New Database**
on the page that loads. A panel for creating a new database will appear on the right of the page. By creating a database, we are creating a logical container to contain one or more collections. 
 
![New database](https://user-images.githubusercontent.com/11193045/112724938-599a3000-8f1e-11eb-86fc-d6089fda6aba.png)

On the **New Database** panel, enter the **Database id** and click **OK**.  

![New database panel](https://user-images.githubusercontent.com/11193045/112724958-6c146980-8f1e-11eb-9e52-65be290b67de.png)

After loading, the new database will display on the top left corner of that page. To create a container inside the database, hover over the database name, click on the ellipsis button that appears, and select **New Container**. Alternatively, you can click **New Container** at the top of the database list.

![new container](https://user-images.githubusercontent.com/11193045/112724975-88180b00-8f1e-11eb-809c-a15b93069443.png)

An **Add Container** panel for creating a new database appears on the right of the page. For the **Database**, select **Use existing** and select the database you have just created from the dropdown menu. Enter a unique container ID. The partition key will automatically partition data among multiple servers, so documents with the same value for the partition key will be grouped in one partition. Choose an appropriate partition key for effective horizontal scaling. For this project, we choose “country” as the partition key. Click **OK**. 



If you expand the database, you will see that a container has been created inside the database.




## Create a .NET API and Connect to the Database
### Create a .NET API
We now create a new standalone .NET Web API using Visual Studio 2019 and C#, and connect to the Cosmos DB account.

First, open Visual Studio and select **Create New Project** in the startup page. 



In the **Create a new project** step, filter to (**C#, All Platforms, Web**), then select **ASP.NET Core Web Application** and click **Next**. 

