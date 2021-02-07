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