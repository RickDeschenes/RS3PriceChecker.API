using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RS3PriceChecker.Entities;
using RS3PriceChecker.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RS3PriceChecker.API.Controllers
{
    [Route("api/ItemDetail")]
    [ApiController]
    public class ItemDetailController : ControllerBase
    {
        private readonly ILogger<ItemDetailController> _logger;

        private readonly IItemDetailService _ItemDetailService;

        public ItemDetailController(IItemDetailService itemDetailService, ILogger<ItemDetailController> logger)
        {
            _logger = logger;
            _ItemDetailService = itemDetailService;
        }

        #region Update GE Item Detailss

        [HttpGet("UpdateItemDetails")]
        public void UpdateItemDetails()
        {
            _ItemDetailService.UpdateItemDetails();
        }

        #endregion

        #region Load Price Files

        [HttpGet("LoadGEPrices")]
        public void LoadGEPrices(string path = @"D:\GEItemDate\")
        {
            _ItemDetailService.LoadGEPrices(path);
        }

        #endregion

        [HttpGet]
        public ItemDetail GetItemDetail(int item)
        {
            return _ItemDetailService.GetItemDetail(item);
        }

        [HttpPost("CreateItemDetail")]
        public ItemDetail CreateItemDetail(ItemDetail request)
        {
            return _ItemDetailService.CreateItemDetail(request);
        }

        [HttpPost("UpdateItemDetail")]
        public ItemDetail UpdateItemDetail(ItemDetail request)
        {
            return _ItemDetailService.UpdateItemDetail(request);
        }

    }
}
