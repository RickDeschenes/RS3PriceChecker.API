using RS3PriceChecker.Models;
using System;

namespace RS3PriceChecker.Services
{
    public interface IItemDetailService
    {
        void UpdateItemDetails();

        void LoadGEPrices(string path);

        ItemDetails GetItemDetail(int item);

        ItemDetails CreateItemDetail(ItemDetails request);

        ItemDetails UpdateItemDetail(ItemDetails request);
    }
}