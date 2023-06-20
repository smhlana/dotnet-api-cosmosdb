using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using CosmosDBCitiesTutorial.Models;
using System;

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

            var rand = new Random();
            if (rand.Next(1, 5) == 3)
            {
                var crashVar = 0;
                crashVar = 100 / crashVar;
            }

            return results;
        }
    }
}