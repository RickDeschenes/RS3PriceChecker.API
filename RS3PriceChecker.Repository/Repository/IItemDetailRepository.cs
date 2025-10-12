using RS3PriceChecker.Models;

namespace RS3PriceChecker.Repository
{
    public interface IItemDetailRepository
    {
        string UpdateItems();

        List<Items> GetAllItems();

        List<Prices> GetAllPricesByItemID(int ID);

        List<MostRecentPrices> GetRecentPrices();

        ItemDetails GetItemDetail(int ID);

        ItemDetails CreateItemDetail(ItemDetails request);

        ItemDetails UpdateItemDetail(ItemDetails request);

        void CreatePrice(Prices prices);

        Categories GetCatagoryByID(int ID);

        Categories GetCatagoryByName(string Name);

        List<Categories> GetCatagories();
    }
}