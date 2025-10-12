using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RS3PriceChecker.Services
{
    public class GrandExchangeService
    {

        public string GetItems(int category, string startsWith, int page)
        {
            string url = string.Format("http://services.runescape.com/m=itemdb_rs/api/catalogue/items.json?category={0}&alpha={1}&page={2}", category, startsWith, page);
            try
            {
                return Utilities.GetRuneScapeResponse(url);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetDetail(int itemID)
        {
            string url = string.Format("http://services.runescape.com/m=itemdb_rs/api/catalogue/detail.json?item={0}", itemID);
            try
            {
                return Utilities.GetRuneScapeResponse(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public int GetCatalogueCount()
        {
            int results = -1;
            string url = string.Format("https://secure.runescape.com/m=itemdb_rs/catalogue");
            string catalog = "<li><a href='catalogue?cat=";

            try
            {
                var raw = Utilities.GetRuneScapeResponse(url);
                results = raw.Split(catalog).Length - 1;
            }
            catch (Exception e)
            {
                return results;
            }
            return results;
        }

        public string GetCatalogue(int category)
        {
            string url = string.Format("http://services.runescape.com/m=itemdb_rs/api/catalogue/category.json?category={0}", category);

            try
            {
                return Utilities.GetRuneScapeResponse(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string GetPrices(int itemID)
        {
            string url = string.Format("http://services.runescape.com/m=itemdb_rs/api/graph/{0}.json", itemID);
            try
            {
                return Utilities.GetRuneScapeResponse(url);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
