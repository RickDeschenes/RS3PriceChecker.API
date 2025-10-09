using RS3PriceChecker.Entities;
using System;

namespace RS3PriceChecker.Services
{
    public interface IItemDetailService
    {
        void UpdateItemDetails();

        void LoadGEPrices(string path);

        ItemDetail GetItemDetail(int item);

        ItemDetail CreateItemDetail(ItemDetail request);

        ItemDetail UpdateItemDetail(ItemDetail request);
    }
}