using CustomLogger;

namespace RS3PriceChecker.Services.Services;

public interface IGrandExchangeService
{
    string GetDownload(string url = "https://chisel.weirdgloop.org/gazproj/gazbot/rs_dump.json");
    string GetItems(int category, string startsWith, int page);
    string GetDetail(int itemID);
    int GetCatalogueCount();
    string GetCatalogue(int category);
    string GetPrices(int itemID);
}

public class GrandExchangeService(ICustomLogger logger)
{
    private readonly ICustomLogger _logger = logger;

    public string GetDownload(string url = "https://chisel.weirdgloop.org/gazproj/gazbot/rs_dump.json")
    {
        try
        {
            var allData = Utilities.GetRuneScapeResponse(url);
            return allData;
        }
        catch (Exception e)
        {
            _logger.Error($"Error downloading data: {e.Message}.", e);
            throw new Exception(e.Message);
        }
    }

    public string GetItems(int category, string startsWith, int page)
    {
        string url = string.Format("http://services.runescape.com/m=itemdb_rs/api/catalogue/items.json?category={0}&alpha={1}&page={2}", category, startsWith, page);
        try
        {
            return Utilities.GetRuneScapeResponse(url);
        }
        catch (Exception e)
        {
            _logger.Error($"Error getting items: {e.Message}.", e);
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
            _logger.Error($"Error getting item detail: {e.Message}.", e);
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
            _logger.Error($"Error getting catalogue count: {e.Message}.", e);
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
            _logger.Error($"Error getting catalogue: {e.Message}.", e);
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
            _logger.Error($"Error getting prices: {e.Message}.", e);
            return e.Message;
        }
    }
}
