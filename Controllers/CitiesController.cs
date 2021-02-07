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

		[HttpGet()]     // Get: api/cities
		[ActionName("Index")]
		public async Task<IEnumerable<Item>> Index()	// Returns all documents stored in the DB
		{
			return await _cosmosDbService.GetItemsAsync("SELECT * FROM c");
		}

		[HttpPost("create")]    // Post: api/cities/create
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