using Microsoft.Extensions.Logging;
using RS3PriceChecker.Database;
using RS3PriceChecker.Entities;
using RS3PriceChecker.Mapper;
using RS3PriceChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RS3PriceChecker.Repository
{
    public class ItemDetailRepository : IItemDetailRepository
    {
        protected readonly RS3PriceCheckerDBContext db;

        private readonly ILogger<ItemDetailRepository> _logger;

        public ItemDetailRepository(ILogger<ItemDetailRepository> logger, RS3PriceCheckerDBContext context)
        {
            db = context;
            _logger = logger;
        }

        #region Update Items

        public string UpdateItems()
        {
            return "";
        }

        #endregion Update Items

        #region Crud Items

        public List<Items> GetAllItems()
        {
            return db.Item.ToList();
        }

        public List<Prices> GetAllPricesByItemID(int itemID)
        {
            return db.Price.Where(w => w.ItemID == itemID).ToList();
        }

        public
        List<MostRecentPrices> GetRecentPrices()
        {
            return db.MostRecentPrice.ToList();
        }

        public ItemDetail GetItemDetail(int itemID)
        {
            DateTime date = DateTime.Today.AddDays(-1);

            var item = db.Item.Where(w => w.ItemID == itemID).FirstOrDefault();
            var i = db.Icon.Where(w => w.ID == item.IconId).FirstOrDefault();
            var c = db.Category.Where(w => w.ID == item.CategoryId).FirstOrDefault();
            var n = db.Name.Where(w => w.ID == item.NameId).FirstOrDefault();

            var p = db.Price.Where(w => w.ItemID == item.ID).ToList();

            return new ItemDetail
            {
                Catagory = item.Category.Name,
                Date = item.ModifiedDate,
                ItemID = item.ItemID,
                LargeIcon = item.Icon.Large,
                Name = item.Name.Value,
                Description = item.Name.Description,
                Prices = PriceMapper.ModelToEntityList(p),
                SmallIcon = item.Icon.Small
            };

        }

        public ItemDetail CreateItemDetail(ItemDetail request)
        {
            ItemDetail results = new();

            string createBy = "RickD";
            DateTime date = DateTime.Now;

            var c = db.Category.Where(w => w.Name == request.Catagory).FirstOrDefault();
            if (c == null || c.ID <= 0)
                return results;

            var icon = db.Icon.Where(w => w.Small == request.SmallIcon).FirstOrDefault();
            if (icon == null || icon.ID <= 0)
                //we have a new item, store it            
                icon = CreateIcon(new Models.Icons()
                {
                    Small = request.SmallIcon,
                    Large = request.LargeIcon,
                    CreatedBy = createBy,
                    CreatedDate = date,
                    ModifiedBy = createBy,
                    ModifiedDate = date
                });

            var name = db.Name.Where(w => w.Value == request.Name).FirstOrDefault();
            if (name == null || name.ID <= 0)
                name = CreateName(new Models.Names()
                {
                    Value = request.Name,
                    Description = request.Description,
                    CreatedBy = createBy,
                    CreatedDate = date,
                    ModifiedBy = createBy,
                    ModifiedDate = date
                });

            var item = db.Item.Where(w => w.ItemID == request.ItemID).FirstOrDefault();
            if (item == null || item.ID <= 0)
                item = CreateItem(new Models.Items()
                {
                    CategoryId = c.ID,
                    IconId = icon.ID,
                    ItemID = request.ItemID,
                    NameId = name.ID,
                    CreatedBy = createBy,
                    CreatedDate = date,
                    ModifiedBy = createBy,
                    ModifiedDate = date
                });

            if (item != null && item.ID > 0)
                CreatePrices(PriceMapper.EntityToModelList(request.Prices), item.ID, createBy, date);

            return null; //GetItemDetail(item.ItemID);
        }

        private void CreatePrices(List<Prices>  rawprices, int iD, string createBy, DateTime date)
        {
            List<Prices> prices = new();

            foreach (var p in rawprices)
            {
                var item = db.Price.Where(w => w.ItemID == iD && w.Date == p.Date).FirstOrDefault();
                if (item == null || item.ID <= 0)
                {
                    p.ItemID = iD;
                    p.CreatedBy = createBy;
                    p.CreatedDate = date;
                    p.ModifiedBy = createBy;
                    p.ModifiedDate = date;
                    prices.Add(p);
                }
            }

            using var transaction = db.Database.BeginTransaction();
            db.Price.AddRange(prices);
            db.SaveChanges();
            db.Database.CommitTransaction();

        }

        public void CreatePrice(Prices price)
        {
            using var transaction = db.Database.BeginTransaction();
            db.Price.Add(price);
            db.SaveChanges();
            db.Database.CommitTransaction();
        }

        public Categories GetCatagoryByID(int ID)
        {
            return db.Category.Where(w => w.ID == ID).FirstOrDefault();
        }

        public Categories GetCatagoryByName(string Name)
        {
            return db.Category.Where(w => w.Name == Name).FirstOrDefault();
        }

        public List<Categories> GetCatagories()
        {
            return db.Category.Where(w => !w.Name.StartsWith("Deleted - ")).ToList();
        }

        private Models.Icons GetIcon(string small)
        {
            return db.Icon.Where(w => w.Small == small).FirstOrDefault();
        }

        private Models.Icons CreateIcon(Models.Icons icon)
        {
            using var transaction = db.Database.BeginTransaction();
            db.Icon.Add(icon);
            db.SaveChanges();
            db.Database.CommitTransaction();

            return GetIcon(icon.Small);
        }


        private Models.Items GetItem(int itemID)
        {
            return db.Item.Where(w => w.ItemID == itemID).FirstOrDefault();
        }

        private Models.Items CreateItem(Models.Items item)
        {
            using var transaction = db.Database.BeginTransaction();
            db.Item.Add(item);
            db.SaveChanges();
            db.Database.CommitTransaction();

            return GetItem(item.ItemID);
        }

        private Models.Names GetName(string value)
        {
            return db.Name.Where(w => w.Value == value).FirstOrDefault();
        }

        private Models.Names CreateName(Models.Names name)
        {
            using var transaction = db.Database.BeginTransaction();
            db.Name.Add(name);
            db.SaveChanges();
            db.Database.CommitTransaction();

            return GetName(name.Value);
        }

        public ItemDetail UpdateItemDetail(ItemDetail request)
        {
            ItemDetail results = new();

            return results;
        }

        #endregion 

    }
}
