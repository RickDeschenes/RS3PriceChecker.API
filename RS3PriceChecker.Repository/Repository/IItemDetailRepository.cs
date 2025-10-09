using RS3PriceChecker.Models;
using RS3PriceChecker.Entities;
using System.Collections.Generic;

namespace RS3PriceChecker.Repository
{
    public interface IItemDetailRepository
    {
        string UpdateItems();

        List<Items> GetAllItems();

        List<Prices> GetAllPricesByItemID(int ID);

        List<MostRecentPrices> GetRecentPrices();

        ItemDetail GetItemDetail(int ID);

        ItemDetail CreateItemDetail(ItemDetail request);

        ItemDetail UpdateItemDetail(ItemDetail request);

        void CreatePrice(Prices prices);

        Categories GetCatagoryByID(int ID);

        Categories GetCatagoryByName(string Name);

        List<Categories> GetCatagories();
    }
}