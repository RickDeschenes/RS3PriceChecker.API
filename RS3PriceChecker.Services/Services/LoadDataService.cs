using Microsoft.Extensions.Logging;
using Ety = RS3PriceChecker.Database;
using RS3PriceChecker.Models;
using RS3PriceChecker.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace RS3PriceChecker.Services.Services
{
    public class LoadDataService
    {
        private bool Delete { get; set; }
        private string StatusMessage { get; set; }
        private int Current { get; set; }
        private int Iterations { get; set; }
        private int SleepSeconds { get; set; }
        private int StatusCode { get; set; }
        private string OutputPath { get; set; }
        private List<RSItem> Items { get; set; }

        private readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

        private readonly string[] Categories = ["Miscellaneous", "Ammo", "Arrows", "Bolts", "Construction materials", "Construction products", "Cooking ingredients", "Costumes", "Crafting materials", "Familiars", "Farming produce", "Fletching materials", "Food and Drink", "Herblore materials", "Hunting equipment", "Hunting Produce", "Jewellery", "Mage armour", "Mage weapons", "Melee armour - low level", "Melee armour - mid level", "Melee armour - high level", "Melee weapons - low level", "Melee weapons - mid level", "Melee weapons - high level", "Mining and Smithing", "Potions", "Prayer armour", "Prayer materials", "Range armour", "Range weapons", "Runecrafting", "Runes, Spells and Teleports", "Seeds", "Summoning scrolls", "Tools and containers", "Woodcutting product", "Pocket items", "Stone spirits", "Salvage", "Firemaking products", "Archaeology materials"];

        private readonly IGrandExchangeService _exchangeService;
        private readonly ILogger<LoadDataService> _logger;
        private readonly ItemDetailRepository _ItemDetailRepository;
        private readonly ItemDetailService _itemDetailService;

        /// <summary>
        /// LoadDataService
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="itemDetailRepository"></param>
        public LoadDataService(ILogger<LoadDataService> logger, ItemDetailService itemDetailService, ItemDetailRepository itemDetailRepository, IGrandExchangeService exchangeService)
        {
            _logger = logger;
            _itemDetailService = itemDetailService;
            _ItemDetailRepository = itemDetailRepository;
            _exchangeService = exchangeService;

            StatusMessage = "Calls to fast";
            StatusCode = Iterations;
            Iterations = 5;
            SleepSeconds = 3000;
            Current = 0;
            OutputPath = @"C:\Output\";

            Items = [];
        }

        #region Get Items

        public string GetPrices(List<int> items)
        {
            var results = new StringBuilder();
            foreach (int i in items)
            {
                var item = new RawPrice()
                {
                    Id = i,
                    Price = new()
                };
                var vals = LoadPrice(i).ToList();
                item.Price = vals.Where(x => x.Date == DateTime.Today || x.Date == DateTime.Today.AddDays(-1)).First().Price;
                var result = JsonSerializer.Serialize(item, Options);

                results.AppendLine(result);
            }
            return results.ToString();
        }

        #endregion Get Items

        #region Load GE Items

        public void LoadAllGEItems(string outputPath)
        {
            _logger.LogInformation("Main Call to update Items.");

            Items = [];

            OutputPath = outputPath;
            _logger.LogInformation("{data}", LoadData());
        }

        private string LoadData()
        {
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();

            OutputPath = CreateDirectory(OutputPath);

            for (int i = 0; i < 42; i++)
            {
                LoadFilters(i);
            }

            watch.Stop();
            TimeSpan span = watch.Elapsed;
            string processTime = string.Format("{0}:{1}:{2}", span.Hours, span.Minutes, span.Seconds);
            _logger.LogInformation("Processed in h:mm:ss: {time}.", processTime);

            return string.Format("Processed {0} categories for a total of {1} items in {2} h:mm:ss.", Categories.Length, Items.Count, processTime);
        }

        private void LoadFilters(int category)
        {
            string catalog = Categories[category];
            string extention = ".json";
            string raw = string.Empty;
            int attempts = 1;

            while (raw == string.Empty)
            { 
                raw = _exchangeService.GetCatalogue(category);
                if (raw == string.Empty)
                    Thread.Sleep(SleepSeconds);
                if (attempts > 5)
                {
                    string Message = string.Format("Processing Get Catalogue: {0} category: {1} with a ThreadSleep of: {2} ms.", catalog, category, SleepSeconds);
                    //throw an exception                        
                    var ex = new Exception(string.Format("{0} - {1}", StatusMessage, StatusCode));
                    ex.Data.Add(StatusCode, "To fast");
                    throw ex;
                }
                attempts++;
            }
            Current++;

            var vals = JsonSerializer.Deserialize<FilterValues>(raw, Options);
            if (vals == null || vals.Alpha == null)
                return;

            foreach (var item in vals.Alpha.Where(w => w.Items > 0 && w.Letter != "#"))
            {
                string path = Path.Combine(OutputPath, string.Format("{0}_{1}{2}", catalog, item.Letter, extention));

                //if the file exists and delete is on
                if (File.Exists(path) && Delete)
                    try { File.Delete(path); }
                    catch (Exception ex) { _logger.LogError(ex, "{message}.", ex.Message); }

                //reset the items list
                Items = [];

                //If the file does not exist, process it
                if (!File.Exists(path))
                {
                    LoadItems(category, item.Letter, item.Items);
                    var output = JsonSerializer.Serialize(Items, Options );

                    File.WriteAllText(path, output);
                }
            }

        }

        private string CreateDirectory(string outputPath)
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
            catch (Exception ex) { _logger.LogError(ex, "{message}.", ex.Message); }

            //on error return original value
            return outputPath;

        }

        private string LoadItems(int category, string letter, double items)
        {
            if (letter == null || letter.Length != 1)
                return "";

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int pages = 1;
            if (items > 24)
                pages = (int)Math.Ceiling(items / 24);

            for (int page = 1; page <= pages; page++)
            {
                int attempts = 1;
                Thread.Sleep(SleepSeconds);
                string vals = _exchangeService.GetItems(category, letter, page);

                while (vals == string.Empty)
                {
                    attempts++;
                    Thread.Sleep(SleepSeconds);
                    vals = _exchangeService.GetItems(category, letter, page);
                    if (attempts >= 5 && (string.IsNullOrEmpty(vals) || vals == "null"))
                    {
                        _logger.LogInformation("Processing catagory: {category} letter: {letter} page {page} of {pages} with a ThreadSleep of: {sleep} ms.", category, letter, page, Items.Count, SleepSeconds);
                        var ex = new Exception(string.Format("{0} - {1}", StatusMessage, StatusCode));
                        ex.Data.Add(StatusCode, "To fast");
                        throw ex;
                    }
                }
                Current++;

                var data = JsonSerializer.Deserialize<RSRawItems>(vals, Options);
                if (data == null || data.Items == null || data.Items.Count == 0)
                    continue;

                data.Items = SetCategory(data.Items, Categories[category]);

                Items.AddRange(data.Items);
            }

            LoadPrices(category);

            string results = JsonSerializer.Serialize(Items, Options);

            watch.Stop();
            TimeSpan span = watch.Elapsed;
 
            _logger.LogInformation("Processed {items} items in h:mm:ss: {time}.", Items.Count, string.Format("{0}:{1}:{2}", span.Hours, span.Minutes, span.Seconds));

            return results;
        }

        private static List<RSItem> SetCategory(List<RSItem> items, string category)
        {
            foreach (var item in items)
            {
                item.Date = DateTime.Today.AddDays(-1);
                item.Category = category;
            }

            return items;
        }

        private void LoadPrices(int catagory)
        {
            foreach (var item in Items)
            {
                int attempts = 1;
                Thread.Sleep(SleepSeconds);

                var vals = LoadPrice(item.Id);
                attempts = 1;
                while (vals == null || vals.Count <= 0)
                {
                    _logger.LogWarning("No Data Returned Catagory:Current {Catagory}:{Current}::{attempts}.", catagory, Current, attempts);
                    Thread.Sleep(SleepSeconds);
                    vals = LoadPrice(item.Id);
                    attempts += 1;
                    if (attempts >= 5 && (vals == null || vals.Count <= 0))
                    {
                        string Message = string.Format("Processing catagory: {0} itemID: {1} index {2} of {3} with a ThreadSleep of: {4} ms.", Categories[catagory], item.Id, attempts, Items.Count, SleepSeconds);
                        //throw an exception                        
                        var ex = new Exception(string.Format("{0} - {1}", StatusMessage, StatusCode));
                        ex.Data.Add(StatusCode, "To fast");
                        throw ex;
                    }
                }

                Current += 1;
                item.Prices = vals;
            }
        }

        private List<RSPrice> LoadPrice(int id)
        {
            List<RSPrice> results = [];

            string vals = _exchangeService.GetPrices(id);
            if (vals == "null" || string.IsNullOrEmpty(vals))
                return results;

            vals = vals.Replace("\"", "");

            vals = vals.Replace("{daily:{", "");
            vals = vals[1..vals.IndexOf('}')];
            string[] commas = vals.Split(',');
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

        #endregion

        #region Load GE Item Files

        public void LoadItemFiles(string path)
        {
            string results = string.Format("Loading data from path{0}.", path);

            _logger.LogInformation("Main Call to update Items.");

            string filter = "*.json";

            var list = Directory.GetFiles(path, filter, SearchOption.AllDirectories);

            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                if (!File.Exists(item))
                    continue;

                var data = File.ReadAllText(item);
                var items = JsonSerializer.Deserialize<List<RSItem>>(data, Options);
                if (items == null || items.Count == 0)
                    continue;

                ProcessItems(items, _itemDetailService);
                var name = Path.GetFileName(item);
                if (string.IsNullOrEmpty(name))
                    continue;

                var processed = Path.GetDirectoryName(item);
                if (processed == null)
                    continue;

                processed = Path.Combine(processed, "Processed");

                if (!Directory.Exists(processed))
                    Directory.CreateDirectory(processed);

                string fullname = Path.Combine(processed, name);

                File.Move(item, fullname);
            }
            //log results
            _logger.LogInformation("{results}", results);
        }

        private static void ProcessItems(List<RSItem> items, ItemDetailService ids)
        {
            foreach (var item in items)
            {
                ProcessItem(item, ids);
            }
        }

        private static void ProcessItem(RSItem item, ItemDetailService ids)
        {
            List<Prices> prices = LoadPrices(item.Prices);

            ids.CreateItemDetail(new ItemDetails()
            {
                Catagory = item.Type,
                Date = item.Date,
                ItemId = item.Id,
                LargeIcon = item.Icon_Large,
                SmallIcon = item.Icon,
                Name = item.Name,
                Description = item.Description,
                Prices = prices
            });
        }

        private static List<Prices> LoadPrices(List<RSPrice> prices)
        {
            List<Prices> results = [];

            foreach (var item in prices)
            {
                results.Add(new Prices()
                {
                    Amount = item.Price,
                    Date = item.Date
                });
            }
            return results;
        }

        #endregion

    }
}
