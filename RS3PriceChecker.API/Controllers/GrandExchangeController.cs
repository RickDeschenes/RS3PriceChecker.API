using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RS3PriceChecker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RS3PriceChecker.API.Controllers
{
    [ApiController]
    [Route("api/GrandExchange")]
    public class GrandExchangeController : ControllerBase
    {
        private readonly ILogger<GrandExchangeController> _logger;
        private readonly GrandExchangeService _GrandExchangeService;

        public GrandExchangeController(ILogger<GrandExchangeController> logger, GrandExchangeService grandExchangeService)
        {
            _logger = logger;
            _GrandExchangeService = grandExchangeService;
        }

        /// <summary>
        /// Retrieves items from the RS item database that match your search.
        /// </summary>
        /// <param name="category">Type of item</param>
        /// <param name="startsWith">The first letter of the item you're searching for</param>
        /// <param name="page">which page is requested</param>
        /// <returns>string</returns>
        [HttpGet("GetGEItems")]
        public string GetGEItems(int category, string startsWith, int page)
        {
            _logger.LogInformation("Call to GetGEItems category:{category}, startsWith:{startsWith}, page:{page}.", category, startsWith, page);
            return _GrandExchangeService.GetItems(category, startsWith, page);
        }

        /// <summary>
        /// Async version of GetItems method.
        /// </summary>
        /// <param name="category">Type of item</param>
        /// <param name="startsWith">The first letter of the item you're searching for</param>
        /// <param name="page">which page is requested</param>
        /// <returns>An awaitable task.</returns>
        [HttpGet("GetGEItemsAsync")]
        public async Task<string> GetGEItemsAsync(int category, string startsWith, int page)
        {
            _logger.LogInformation("Call to GetGEItems {category}, startsWith:{startsWith}, page:{page}.", category, startsWith, page);
            Func<string> asyncFunc = new(() => GetGEItems(category, startsWith, page));
            return await Task.Run(asyncFunc);
        }

        /// <summary>
        /// Gets the information pertaining to an item.
        /// </summary>
        /// <param name="itemID">The Item ID of the item you wish to look up</param>
        /// <returns>string</returns>
        [HttpGet("GetGEItemDetail")]
        public string GetGEItemDetail(int itemID)
        {
            _logger.LogInformation("Call to GetGEItemDetail itemID:{itemID}.", itemID);
            return _GrandExchangeService.GetDetail(itemID);
        }

        /// <summary>
        /// Get Catalogue Count
        /// </summary>
        /// <returns>Number of catalog items</returns>
        [HttpGet("GetCatalogueCount")]
        public int GetCatalogueCount()
        {
            _logger.LogInformation("Call to Get Catalogue count.");
            return _GrandExchangeService.GetCatalogueCount();
        }

        /// <summary>
        /// Finds how many items exist in each category, grouped by letter.
        /// </summary>
        /// <param name="ID">The category ID of the item to search for</param>
        /// <returns>string</returns>
        [HttpGet("GetGECatalogue")]
        public string GetGECatalogue(int ID)
        {
            _logger.LogInformation("Call to GetGECatalogue ID:{ID}.", ID);
            return _GrandExchangeService.GetCatalogue(ID);
        }

        /// <summary>
        /// Async version of GetCatalogue method.
        /// </summary>
        /// <param name="ID">The category ID of the item to search for</param>
        /// <returns>An awaitable task.</returns>
        [HttpGet("GetGECatalogueAsync")]
        public async Task<string> GetGECatalogueAsync(int ID)
        {
            _logger.LogInformation("Call to GetGECatalogueAsync ID:{ID}.", ID);
            return await Task.Run<string>(() => GetGECatalogue(ID));
        }

        /// <summary>
        /// Gets daily or average values for the past 180 days.
        /// </summary>
        /// <param name="ID">The ID of the item you wish to graph.</param>
        /// <returns>string</returns>
        [HttpGet("GetGEPrices")]
        public string GetGEPrices(int ID)
        {
            _logger.LogInformation("Call to GetGECatalogue category:{ID}.", ID);
            return _GrandExchangeService.GetPrices(ID);
        }

        /// <summary>
        /// Async version of GetPrices method.
        /// </summary>
        /// <param name="itemID">The ID of the item you wish to graph.</param>
        /// <returns>An awaitable task.</returns>
        [HttpGet("GetGEPricesAsync")]
        public async Task<string> GetGEPricesAsync(int itemId)
        {
            _logger.LogInformation("Call to GetGEPricesAsync ID:{itemId}.", itemId);
            return await Task.Run<string>(() => GetGEPrices(itemId));
        }
    }
}
