using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Services
{
    public interface IGrandExchangeService
    {
        string GetItems(int category, string startsWith, int page);

        string GetDetail(int itemID);

        int GetCatalogueCount();

        string GetCatalogue(int category);

        string GetPrices(int itemID);
    }
}
