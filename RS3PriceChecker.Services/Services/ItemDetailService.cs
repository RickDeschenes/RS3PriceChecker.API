using CustomLogger;
using RS3PriceChecker.Models;
using RS3PriceChecker.Repository;
using System.Text;

namespace RS3PriceChecker.Services.Services;

public class ItemDetailService(ICustomLogger logger, ItemDetailRepository itemDetailRepository, IGrandExchangeService exchangeService)
{
    private readonly ICustomLogger _logger = logger;
    private readonly ItemDetailRepository _ItemDetailRepository = itemDetailRepository;
    private readonly IGrandExchangeService _exchangeService = exchangeService;

    private string StatusMessage { get; set; } = "Calls to fast";
    private int StatusCode { get; set; } = 900;
    private int LongSleep { get; set; } = 3100;
    private int ShortSleep { get; set; } = 250;

    #region Update Items

    public static void UpdateItemDetails()
    {
        //StringBuilder sb = new();

        //DateTime ltdate = DateTime.Now.Date.AddDays(-1);

        //DateTime dbdate = new DateTimeOffset(DateTime.Today).UtcDateTime.Date;

        //if (dbdate > ltdate)
        //    dbdate = ltdate;

        //var items = _ItemDetailRepository.GetRecentPrices().ToList();
        //items = [.. items.Where(w => w.Date < dbdate)];

        //GrandExchange = new();

        ////foreach (var item in items)
        //for (int i = (items.Count - 1); i >= 0; i--)
        //{
        //    var item = items[i];
        //    var p = _ItemDetailRepository.GetAllPricesByItemID(item.IID).OrderByDescending(d => d.Date).FirstOrDefault();
        //    //If we have prices
        //    if (p != null && p.Id> 0)
        //    {
        //        //and they are older then one day
        //        if (p.Date < dbdate)
        //        {
        //            // Load the prices
        //            sb.AppendLine(UpdatePrices(item.ItemId, p, p.Date));
        //        }
        //    }
        //}
        //log the results
        //return sb.ToString();
    }

    #region Update Items (private)

    private string UpdatePrices(int itemID, Prices prices, DateTime prior)
    {
        string results = "";
        var raw = GetPrices(itemID);
        if (raw.StartsWith("Could not process item") || raw.StartsWith("A connection attempt"))
            return raw;

        var vals = LoadPrices(raw);

        string creator = "RickD";
        DateTime created = new DateTimeOffset(DateTime.Now).UtcDateTime.AddDays(-1); ;

        foreach (var val in vals.Where(w => w.Date > prior).OrderBy(o => o.Date))
        {
            if (val.Date > prior)
            {
                _ItemDetailRepository.CreatePrice(new Prices()
                {
                    Amount = val.Price,
                    Date = val.Date,
                    ItemId = prices.ItemId,
                    CreatedBy = creator,
                    CreatedDate = created,
                    ModifiedBy = creator,
                    ModifiedDate = created
                });
                results += string.Format("Added price for: {0}, for the amount: {1}.", val.Date, val.Price);
            }
            else
                break;
        }

        return results;
    }

    private string GetPrices(int itemID)
    {
        string raw = "null";

        int loops = 0;
        while (raw == null || raw == "null")
        {
            raw = _exchangeService.GetPrices(itemID);
            if (raw == null || raw == "null")
                Thread.Sleep(LongSleep);
            else
                Thread.Sleep(ShortSleep);

            loops++;
        }
        if (raw == null || raw == "null" || !raw.StartsWith('{'))
        {
            string msg = string.Format("Could not load prices for ItemID: {0} after {1} concurent calls, using ShortSleep: {2} and LongSleep: {3} ms.", itemID, loops, ShortSleep, LongSleep);

            //throw an exception                        
            var ex = new Exception(string.Format("{0} - {1}", StatusMessage, StatusCode));
            ex.Data.Add("Details", msg);
            ex.Data.Add(StatusCode, "To fast");
            throw ex;
        }

        return raw;
    }

    private static List<RSPrice> LoadPrices(string prices)
    {
        List<RSPrice> results = [];

        prices = prices.Replace("{\r\n  \"daily\": {\r\n    \"", "");
        prices = prices[..prices.IndexOf("},\r\n  \"average\"")].Replace("\r\n    \"", "").Replace("\": ", ":");
        string[] commas = prices.Split(',');
        foreach (var dates in commas)
        {
            string[] vs = dates.Split(':');
            double ticks = double.Parse(vs[0]);
            int value = int.Parse(vs[1]);
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);

            DateTime date = new DateTime(1970, 1, 1) + time;

            results.Add(new RSPrice()
            {
                Date = date,
                Price = value
            });
        }

        return results;
    }

    #endregion Update Items (private)

    #endregion Update Items

    #region Load Prices

    public void LoadGEPrices(string path)
    {
        LoadPriceFiles(path);
    }

    #region private Load Prices

    private void LoadPriceFiles(string fullpath)
    {
        fullpath = CreateDirectory(fullpath);

        var items = _ItemDetailRepository.GetRecentPrices().ToList();

        Random random = new();
        int occurence = 1;

        for (int i = (items.Count - 1); i >= 0; i--)
        {
            var item = items[i];

            string file = Path.Combine(fullpath, item.ItemId.ToString());
            string extension = ".json";

            string daily = "{\"daily";

            if (!File.Exists(file + extension))
            {
                Thread.Sleep(random.Next(3600));
                string data = _exchangeService.GetPrices(item.ItemId);

                //One extra try
                while (!data.StartsWith(daily))
                {
                    Thread.Sleep(3600);
                    data = _exchangeService.GetPrices(item.ItemId);
                    occurence++;
                    if (!data.StartsWith(daily) && occurence >= 5)
                    {
                        _logger.Debug($"Processing update prices item: {i + 1} if: {items.Count} with SleepSeconds of: {3600} ms.");

                        //throw an exception                        
                        var ex = new Exception(string.Format("{0} - {1}", StatusMessage, StatusCode));
                        ex.Data.Add(StatusCode, "To fast");
                        throw ex;
                    }
                }
                occurence = 1;

                if (data.StartsWith(daily))
                    file += extension;
                else
                    file += "_error" + extension;

                data = data.Replace(daily, "{\r\n  \"ItemID\": " + item.ItemId.ToString() + ",\r\n  \"daily");
                File.WriteAllText(file, data);
            }
        }
    }

    private static string CreateDirectory(string outputPath)
    {
        string folder = string.Format("{0}{1}{2}", DateTime.Today.Year.ToString("0000"), DateTime.Today.Month.ToString("00"), DateTime.Today.Day.ToString("00"));

        string results = Path.Combine(outputPath, folder);

        if (Directory.Exists(results))
            return results;

        try
        {
            if (!Directory.Exists(results))
                if (Directory.CreateDirectory(results).FullName == results)
                    return results;
        }
        catch (Exception) { throw; }

        //on error return original value
        return outputPath;

    }

    #endregion private Load Prices

    #endregion Load Prices

    public ItemDetails GetItemDetail(int item)
    {
        return _ItemDetailRepository.GetItemDetail(item);
    }

    public ItemDetails CreateItemDetail(ItemDetails request)
    {
        return _ItemDetailRepository.CreateItemDetail(request);
    }

    public static ItemDetails UpdateItemDetail(ItemDetails request)
    {

        return ItemDetailRepository.UpdateItemDetail(request);
    }

}
