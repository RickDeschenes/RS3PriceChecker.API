using Microsoft.AspNetCore.Mvc;
using RS3PriceChecker.Models;
using RS3PriceChecker.Services.Services;

namespace RS3PriceChecker.API.Controllers;

[Route("api/ItemDetail")]
[ApiController]
public class ItemDetailController(ItemDetailService itemDetailService) : ControllerBase
{
    private readonly ItemDetailService _ItemDetailService = itemDetailService;

    #region Update GE Item Detailss

    [HttpGet("UpdateItemDetails")]
    public void UpdateItemDetails()
    {
        ItemDetailService.UpdateItemDetails();
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
    public ItemDetails GetItemDetail(int item)
    {
        return _ItemDetailService.GetItemDetail(item);
    }

    [HttpPost("CreateItemDetail")]
    public ItemDetails CreateItemDetail(ItemDetails request)
    {
        return _ItemDetailService.CreateItemDetail(request);
    }

    [HttpPost("UpdateItemDetail")]
    public ItemDetails UpdateItemDetail(ItemDetails request)
    {
        return ItemDetailService.UpdateItemDetail(request);
    }

}
