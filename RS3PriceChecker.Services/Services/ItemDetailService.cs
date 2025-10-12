using RS3PriceChecker.Models;
using RS3PriceChecker.Repository;
using System.Text;

namespace RS3PriceChecker.Services
{
    public class ItemDetailService : IItemDetailService, IDisposable
    {
        private readonly IItemDetailRepository _ItemDetailRepository;
        private bool disposedValue;
        private string StatusMessage { get; set; }
        private int StatusCode { get; set; }
        private int LongSleep { get; set; }
        private int ShortSleep { get; set; }

        private GrandExchangeService GrandExchange { get; set; }

        public ItemDetailService(IItemDetailRepository itemDetailRepository)
        {
            _ItemDetailRepository = itemDetailRepository;
            StatusMessage = "Calls to fast";
            StatusCode = 900;
            LongSleep = 3100;
            ShortSleep = 250;
        }

        #region Update Items

        public void UpdateItemDetails()
        {
            StringBuilder sb = new();

            DateTime ltdate = DateTime.Now.Date.AddDays(-1);

            DateTime dbdate = new DateTimeOffset(DateTime.Today).UtcDateTime.Date;

            if (dbdate > ltdate)
                dbdate = ltdate;

            var items = _ItemDetailRepository.GetRecentPrices().ToList();
            items = items.Where(w => w.Date < dbdate).ToList();

            GrandExchange = new();

            //foreach (var item in items)
            for (int i = (items.Count - 1); i >= 0; i--)
            {
                var item = items[i];
                var p = _ItemDetailRepository.GetAllPricesByItemID(item.IID).OrderByDescending(d => d.Date).FirstOrDefault();
                //If we have prices
                if (p != null && p.ID > 0)
                {
                    //and they are older then one day
                    if (p.Date < dbdate)
                    {
                        // Load the prices
                        sb.AppendLine(UpdatePrices(item.ItemID, p, p.Date));
                    }
                }
            }
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
                        ItemID = prices.ItemID,
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
                raw = GrandExchange.GetPrices(itemID);
                if (raw == null || raw == "null")
                    Thread.Sleep(LongSleep);
                else
                    Thread.Sleep(ShortSleep);

                loops++;
            }
            if (raw == null || raw == "null" || !raw.StartsWith("{"))
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

            GrandExchange = new();
            Random random = new();
            int occurence = 1;

            for (int i = (items.Count - 1); i >= 0; i--)
            {
                var item = items[i];

                string file = Path.Combine(fullpath, item.ItemID.ToString());
                string extension = ".json";

                string daily = "{\"daily";

                if (!File.Exists(file + extension))
                {
                    Thread.Sleep(random.Next(3600));
                    string data = GrandExchange.GetPrices(item.ItemID);

                    //One extra try
                    while (!data.StartsWith(daily))
                    {
                        Thread.Sleep(3600);
                        data = GrandExchange.GetPrices(item.ItemID);
                        occurence++;
                        if (!data.StartsWith(daily) && occurence >= 5)
                        {
                            string message = string.Format("Processing update prices item: {0} if: {1} with SleepSeconds of: {2} ms.", i + 1, items.Count, 3500);

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

                    data = data.Replace(daily, "{\r\n  \"ItemID\": " + item.ItemID.ToString() + ",\r\n  \"daily");
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

        public ItemDetails UpdateItemDetail(ItemDetails request)
        {

            return _ItemDetailRepository.UpdateItemDetail(request);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ItemDetailService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
