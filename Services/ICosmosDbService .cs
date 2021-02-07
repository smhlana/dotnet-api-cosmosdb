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