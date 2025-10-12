using Microsoft.Extensions.Logging;
using Ety = RS3PriceChecker.Database;
using RS3PriceChecker.Models;

namespace RS3PriceChecker.Repository;

public class ItemDetailRepository : IItemDetailRepository
{
    protected readonly Ety.RS3PriceCheckerDBContext db;

    private readonly ILogger<ItemDetailRepository> _logger;

    public ItemDetailRepository(ILogger<ItemDetailRepository> logger, Ety.RS3PriceCheckerDBContext context)
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
        return []; //db.Item.ToList();
    }

    public List<Prices> GetAllPricesByItemID(int itemID)
    {
        return []; // db.Price.Where(w => w.ItemID == itemID).ToList();
    }

    public
    List<MostRecentPrices> GetRecentPrices()
    {
        return []; // db.MostRecentPrice.ToList();
    }

    public ItemDetails GetItemDetail(int itemID)
    {
        //DateTime date = DateTime.Today.AddDays(-1);

        //var item = db.Item.Where(w => w.ItemID == itemID).FirstOrDefault();
        //var i = db.Icon.Where(w => w.ID == item.IconId).FirstOrDefault();
        //var c = db.Category.Where(w => w.ID == item.CategoryId).FirstOrDefault();
        //var n = db.Name.Where(w => w.ID == item.NameId).FirstOrDefault();

        //var p = db.Price.Where(w => w.ItemID == item.ID).ToList();

        //return new ItemDetail
        //{
        //    Catagory = item.Category.Name,
        //    Date = item.ModifiedDate,
        //    ItemID = item.ItemID,
        //    LargeIcon = item.Icon.Large,
        //    Name = item.Name.Value,
        //    Description = item.Name.Description,
        //    Prices = PriceMapper.ModelToEntityList(p),
        //    SmallIcon = item.Icon.Small
        //};
        return new();
    }

    public ItemDetails CreateItemDetail(ItemDetails request)
    {
        //ItemDetail results = new();

        //string createBy = "RickD";
        //DateTime date = DateTime.Now;

        //var c = db.Category.Where(w => w.Name == request.Catagory).FirstOrDefault();
        //if (c == null || c.ID <= 0)
        //    return results;

        //var icon = db.Icon.Where(w => w.Small == request.SmallIcon).FirstOrDefault();
        //if (icon == null || icon.ID <= 0)
        //    //we have a new item, store it            
        //    icon = CreateIcon(new Models.Icons()
        //    {
        //        Small = request.SmallIcon,
        //        Large = request.LargeIcon,
        //        CreatedBy = createBy,
        //        CreatedDate = date,
        //        ModifiedBy = createBy,
        //        ModifiedDate = date
        //    });

        //var name = db.Name.Where(w => w.Value == request.Name).FirstOrDefault();
        //if (name == null || name.ID <= 0)
        //    name = CreateName(new Models.Names()
        //    {
        //        Value = request.Name,
        //        Description = request.Description,
        //        CreatedBy = createBy,
        //        CreatedDate = date,
        //        ModifiedBy = createBy,
        //        ModifiedDate = date
        //    });

        //var item = db.Item.Where(w => w.ItemID == request.ItemID).FirstOrDefault();
        //if (item == null || item.ID <= 0)
        //    item = CreateItem(new Models.Items()
        //    {
        //        CategoryId = c.ID,
        //        IconId = icon.ID,
        //        ItemID = request.ItemID,
        //        NameId = name.ID,
        //        CreatedBy = createBy,
        //        CreatedDate = date,
        //        ModifiedBy = createBy,
        //        ModifiedDate = date
        //    });

        //if (item != null && item.ID > 0)
        //    CreatePrices(PriceMapper.EntityToModelList(request.Prices), item.ID, createBy, date);

        return new(); //GetItemDetail(item.ItemID);
    }

    private void CreatePrices(List<Prices>  rawprices, int iD, string createBy, DateTime date)
    {
        //List<Ety.Prices> prices = [];

        //foreach (var p in rawprices)
        //{
        //    var item = db.Price.Where(w => w.ItemID == iD && w.Date == p.Date).FirstOrDefault();
        //    if (item == null || item.ID <= 0)
        //    {
        //        p.ItemID = iD;
        //        p.CreatedBy = createBy;
        //        p.CreatedDate = date;
        //        p.ModifiedBy = createBy;
        //        p.ModifiedDate = date;
        //        prices.Add(p);
        //    }
        //}

        //using var transaction = db.Database.BeginTransaction();
        //db.Price.AddRange(prices);
        //db.SaveChanges();
        //db.Database.CommitTransaction();

    }

    public void CreatePrice(Prices price)
    {
        //using var transaction = db.Database.BeginTransaction();
        //db.Price.Add(price);
        //db.SaveChanges();
        //db.Database.CommitTransaction();
    }

    public Categories GetCatagoryByID(int ID)
    {
        return new(); // db.Category.Where(w => w.ID == ID).FirstOrDefault();
    }

    public Categories GetCatagoryByName(string Name)
    {
        return new(); // db.Category.Where(w => w.Name == Name).FirstOrDefault();
    }

    public List<Categories> GetCatagories()
    {
        return new(); // db.Category.Where(w => !w.Name.StartsWith("Deleted - ")).ToList();
    }

    private Icons GetIcon(string small)
    {
        return new(); // db.Icon.Where(w => w.Small == small).FirstOrDefault();
    }

    private Icons CreateIcon(Icons icon)
    {
        //using var transaction = db.Database.BeginTransaction();
        //db.Icon.Add(icon);
        //db.SaveChanges();
        //db.Database.CommitTransaction();

        return new(); // GetIcon(icon.Small);
    }


    private Items GetItem(int itemID)
    {
        return new(); //db.Item.Where(w => w.ItemID == itemID).FirstOrDefault();
    }

    private Items CreateItem(Items item)
    {
        //using var transaction = db.Database.BeginTransaction();
        //db.Item.Add(item);
        //db.SaveChanges();
        //db.Database.CommitTransaction();

        return new(); //GetItem(item.ItemID);
    }

    private Names GetName(string value)
    {
        return new(); //db.Name.Where(w => w.Value == value).FirstOrDefault();
    }

    private Names CreateName(Names name)
    {
        //using var transaction = db.Database.BeginTransaction();
        //db.Name.Add(name);
        //db.SaveChanges();
        //db.Database.CommitTransaction();

        return new(); //GetName(name.Value);
    }

    public ItemDetails UpdateItemDetail(ItemDetails request)
    {
        ItemDetails results = new();

        return results;
    }

    List<Items> IItemDetailRepository.GetAllItems()
    {
        throw new NotImplementedException();
    }

    List<Prices> IItemDetailRepository.GetAllPricesByItemID(int ID)
    {
        throw new NotImplementedException();
    }

    #endregion

}
