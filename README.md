# dotnet-api-cosmosdb
Connect to an Azure Cosmos DB account from a .NET Core Web API using C#

## Introduction
Non-relational databases, also known as NoSQL databases, provide flexible schemas and scale easily with large amounts of data. NoSQL databases don’t store data in relational tables, instead nesting related data within a single structure or document. Azure Cosmos DB is a fully-managed NoSQL database.

This article demonstrates how to configure and use an Azure Cosmos DB SQL account. We will create and query Cosmos DB resources from a .NET Core API using C# and the Microsoft.Azure.Cosmos library running on .NET 5.0. We also use Postman to test the API. In our example, we create a database to store information about cities, add a city to the database, and query that information. 

## Prerequisites
For this project, you need:
  - Visual Studio 2019. Download and install Visual Studio 2019 Community Edition (https://visualstudio.microsoft.com/downloads/).
  - .NET 5.0 SDK. Download it from Microsoft (https://dotnet.microsoft.com/download/dotnet/5.0).
  - Postman to test the web API. Download it from Postman (https://www.postman.com/downloads/).
  - An active Microsoft Azure account. If you don’t have one already, set one up on the Microsoft website (https://azure.microsoft.com/en-ca/free/).

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

![create container_ok](https://user-images.githubusercontent.com/11193045/112725173-826ef500-8f1f-11eb-8fb2-07bf77039316.PNG)

If you expand the database, you will see that a container has been created inside the database.

![conainer created](https://user-images.githubusercontent.com/11193045/112725212-c06c1900-8f1f-11eb-86e9-c165c1266e71.PNG)

## Create a .NET API and Connect to the Database
### Create a .NET API
We now create a new standalone .NET Web API using Visual Studio 2019 and C#, and connect to the Cosmos DB account.

First, open Visual Studio and select **Create New Project** in the startup page. 

![create a new project](https://user-images.githubusercontent.com/11193045/112725226-d679d980-8f1f-11eb-94a3-8892f8acb502.png)

In the **Create a new project** step, filter to (**C#, All Platforms, Web**), then select **ASP.NET Core Web Application** and click **Next**. 

![create a new project_filter](https://user-images.githubusercontent.com/11193045/112725249-eb566d00-8f1f-11eb-80fd-b7df7ad2a144.png)

In **Configure your new project**, name the project, choose a location, and click **Create**.

![configure your new project](https://user-images.githubusercontent.com/11193045/112725279-0cb75900-8f20-11eb-9ae4-dcc17dd23ea3.png)

In the **Create a new ASP.NET Core web application** dialog, select **API** and click **Create**. You can change the target framework to ASP.NET Core 5.0 on this dialog, or you can change it later, as you will see in the next step.

![create API](https://user-images.githubusercontent.com/11193045/112725310-307a9f00-8f20-11eb-8647-db43da4073c9.png)

When the new project loads, change the target framework to .NET Core 5.0. You can do this in the **Project Explorer**. Right click on the project (**CosmosDBCitiesTutorial**) and click on **Properties**. 

Under the **Application** tab, choose **.NET Core 5.0** from the **Target framework** dropdown. If you do not have .NET Core 5.0 installed, choose **Install other frameworks** in the dropdown menu and you will be directed to a website where you can download and install it. Make sure you save the changes when that is done.

![change target framework](https://user-images.githubusercontent.com/11193045/112725418-cf9f9680-8f20-11eb-8e4e-8c840966cba3.png)

We will now install the Azure Cosmos DB library. We will use this to connect to the database using the SQL API. Again, right click on the project and select **Manage NuGet Packages**. In the NuGet package manager, select the **Browse** tab and search for **Microsoft.Azure.Cosmos** and select it, then click **Install**.

![install library](https://user-images.githubusercontent.com/11193045/112725455-0c6b8d80-8f21-11eb-9987-442ba8aecbbe.png)

Select **OK** in the **Preview Changes** dialog that appears, then accept the license agreement in the **License Acceptance** that appears next.

In the code, we need to use the Azure Cosmos DB connection details. We use the appsettings.json application configuration file to store configuration settings such as database connection strings, account keys, and more. Find this file in the solution explorer on the root of the project folder. 

![appsettings](https://user-images.githubusercontent.com/11193045/112725483-31f89700-8f21-11eb-8f5c-2db7ee2387a4.PNG)

Open appsettings.json and add the CosmosDb key, as shown below, under AllowedHosts.

"AllowedHosts": "*",
"CosmosDb": {
   	"Account": "",
       "Key": "",
       "DatabaseName": "",
       "ContainerName": ""
 }

The _DatabaseName_ is the name of the account, “CitiesDB”, and the CotainerName is the name of the container, “Cities”. The Account is the endpoint URI of the account. To find this, go to your Azure Cosmos DB account, in the overview section. The Key is the PRIMARY KEY found in the account page under **Settings > Keys**. The configuration file should now look like this:

![appsettings_new key](https://user-images.githubusercontent.com/11193045/112725817-bdbef300-8f22-11eb-8876-43144752213e.PNG)

The setup is complete. Now, let’s get on with the code.

## Create a Cosmos DB Service and Connect to the Database
Because we want to separate the database logic from the controller, we require a class containing the logic to connect to the database. We add a class called CosmosDbService and an interface called ICosmosDbService and put these inside a folder called **Services**.
To do this, first create the folder. Right-click on the project in **Solution Explorer** and select **Add > New Folder**. 

Add the two classes into the folder. To add a class, right-click on the folder and select **Add > Class** and name the class, for example, _CosmosDbService.cs_ or _ICosmosDbService.cs_. 

Add the following code in _CosmosDbService_.cs:

	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.Azure.Cosmos;
	using CosmosDBCitiesTutorial.Models;

	namespace CosmosDBCitiesTutorial.Services
	{
		public class CosmosDbService : ICosmosDbService
		{
			private Container _container;

			public CosmosDbService(CosmosClient dbClient, string databaseName, string containerName)
			{
				this._container = dbClient.GetContainer(databaseName, containerName);
			}

			public async Task AddItemAsync(Item item)
			{
				await this._container.CreateItemAsync<Item>(item, new PartitionKey(item.Country));
			}

			public async Task<IEnumerable<Item>> GetItemsAsync(string queryString)
			{
				var query = this._container.GetItemQueryIterator<Item>(new QueryDefinition(queryString));
				List<Item> results = new List<Item>();
				while (query.HasMoreResults)
				{
				var response = await query.ReadNextAsync();

				results.AddRange(response.ToList());
				}

				return results;
			}
		}
	}

Add the following code to _ICosmosDbService.cs_:

	using System.Collections.Generic;
	using System.Threading.Tasks;
	using CosmosDBCitiesTutorial.Models;

	namespace CosmosDBCitiesTutorial.Services
	{
		public interface ICosmosDbService
		{
		    Task AddItemAsync(Item item);
		    Task<IEnumerable<Item>> GetItemsAsync(string query);
		}
	}

By now you should get an error stating that Item cannot be found. To fix this, now we have to add a model defining the data schema. Add a folder called _Models_. Right-click on the project in **Solution Explorer** and select **Add > New Folder**. 

Inside the folder, create a file called _Item.cs_. Add the following code to Item.cs:

	using Newtonsoft.Json;

	namespace CosmosDBCitiesTutorial.Models
	{
		public class Item
		{
		    [JsonProperty(PropertyName = "id")]
		    public string Id { get; set; }

		    [JsonProperty(PropertyName = "name")]
		    public string Name { get; set; }

		    [JsonProperty(PropertyName = "country")]
		    public string Country { get; set; }

		    [JsonProperty(PropertyName = "zipcode")]
		    public string ZipCode { get; set; }
		}
	}

To access the configuration information in _appsettings.json_ within the _Startup_ class, found in _Startup.cs_, you need to use the _IConfiguration_ service provided by .NET. We must add a method which reads the configuration information as follows:

	private static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
     	{
        	string databaseName = configurationSection.GetSection("DatabaseName").Value;
                string containerName = configurationSection.GetSection("ContainerName").Value;
                string account = configurationSection.GetSection("Account").Value;
                string key = configurationSection.GetSection("Key").Value;
                Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
                CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
                Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
                await database.Database.CreateContainerIfNotExistsAsync(containerName, "/country");

                return cosmosDbService;
	}

Add the namespace for the **CosmosDbService** in the Using section.

	using CosmosDBCitiesTutorial.Services;

We initialize the client as a singleton instance based on the configuration information in the CosmosDb key in the appsettings.json file. In that same class, replace the _ConfigureServices_ method as follows:

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddControllers();

		services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(Configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
	}

## Adding Resources to the Database and Querying
We now add items to the database, as well as query those items, with the help of the Azure Cosmos DB library.

To do this, we create a controller to handle incoming HTTP requests and responses. The controller will receive requests from the client and communicate with the database via the CosmosDbService, sending the response back to the client.

To add the controller, right-click on the **Controllers** folder and select **Add > Controller**. In **Add New Scaffold Item** under **Common > API**, select **API Controller – Empty** and click **Add**. Name the controller _CitiesController.cs_ and click **Add**. The name of the controller must end with “Controller”. Replace the controller with this code:

	using Microsoft.AspNetCore.Mvc;
	using CosmosDBCitiesTutorial.Services;
	using System.Threading.Tasks;
	using CosmosDBCitiesTutorial.Models;
	using System.Collections.Generic;
	using System;

	namespace CosmosDBCitiesTutorial.Controllers
	{
		[Route("api/[controller]")]
		[ApiController]
		public class CitiesController : ControllerBase
		{
			private readonly ICosmosDbService _cosmosDbService;
			public CitiesController(ICosmosDbService cosmosDbService)
			{
				_cosmosDbService = cosmosDbService;
			}

			[HttpGet()] 	// Get: api/cities
			[ActionName("Index")]
			public async Task<IEnumerable<Item>> Index()	// Returns all documents stored in the DB
			{
				return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
			}

			[HttpPost("create")]	// Post: api/cities/create
			[ActionName("Create")]
			public async Task<ActionResult> CreateAsync([Bind("Id,Name,Country,ZipCode")] Item item)
			{
				if (ModelState.IsValid)
				{
				item.Id = Guid.NewGuid().ToString();
				await _cosmosDbService.AddItemAsync(item);
					return RedirectToAction("Index");   // Returns updated list of documents stored in the DB
				}

			return StatusCode(500);
			}
		}
	}

Our API is now ready to access data from the database, but there is one more thing we must do. We set the launchUrl to point to our controller. Open **Properties > launchSettings.json** and change the _launchUrl_ property to “_api/cities_” in both profiles. You can also delete _WeatherForecastController.cs_ under the **Controllers** folder and _WeatherForecast.cs_ on the root. 

## Results
Press F5 to run the application. You should see an empty array appear on the screen. That is because we have not added items to the database. When you run the application the Index method is called because we specified the launchUrl to be “api/cities”. The Index method queries the database for all the documents. 

To add an item, we use Postman, so first start Postman. **Disable SSL certificate verification** because Postman does not attempt to access the user’s certificate store to validate the Security Controls **ST Root Authority** certificate. As such, it fails to make a secure SSL connection to the Security Controls console server, resulting in the error “Error: Unable to verify the first certificate”. To disable verification, click **File > Settings**. In the **General** tab, toggle the **SSL certificate verification** to off and close **Settings**.

We now create a POST request to the API to add an item. Select **Create a request** under **Get started**. Set the HTTP method to **POST**. Set the request URL to https://localhost:44376/api/cities/create. Ensure the port number matches that of your local instance. Select the **Body** tab then the **raw** radio button. Click on **Text** to show other options and select **JSON**. Enter the body of the request, as shown below, and click **Send**. 

	{
		"name": "Cape Town",
		"country": "South Africa",
		"zipcode": "8001"
	}

![postman_post](https://user-images.githubusercontent.com/11193045/112726332-7be37c00-8f25-11eb-80d6-83e56fdbb60e.PNG)

You should get a response like this:

![postman_post_response](https://user-images.githubusercontent.com/11193045/112726335-8271f380-8f25-11eb-8c29-cf65e9a76cf1.PNG)

Now, if you go to your browser and type the url https://localhost:44376/api/cities, you should see a JSON response with the city you just added. Again, ensure the port number matches that of your local instance.

![postman_post_response_browser](https://user-images.githubusercontent.com/11193045/112726357-a03f5880-8f25-11eb-9c8e-801c52af0d08.PNG)

There you have it. We can add items to the database and also query the data stored there using our API.

## Conclusion
This article demonstrated how to connect to an Azure Cosmos DB account using a .NET API. We first had to create the database account, together with the database and container in Azure portal. Then, we created an API that uses a controller and a service to communicate with the database and the client. The result is an application that can access data stored in Azure Cosmos DB. 

Use your new knowledge to create your own database. Or, expand on the techniques you learned here to add even more database-related features to your app.

To learn more, check out these useful resources:
Azure Cosmos DB documentation (https://docs.microsoft.com/en-us/azure/cosmos-db/)
Partitioning and horizontal scaling in Azure Cosmos DB (https://docs.microsoft.com/en-us/azure/cosmos-db/partitioning-overview)
