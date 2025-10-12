using AutoMapper;
using Microsoft.Extensions.Logging;
using RS3PriceChecker.Database;
using RS3PriceChecker.Models;

namespace RS3PriceChecker.Repository;

public class ItemDetailRepository
{
    protected readonly RS3DbContext db;

    private readonly IMapper _mapper;

    public ItemDetailRepository(RS3DbContext context, IMapper mapper)
    {
        db = context;
        _mapper = mapper;
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
        return _mapper.Map<List<Item>, List<Items>>([.. db.Item]);
    }

    public List<Prices> GetAllPricesByItemID(int itemID)
    {
        return _mapper.Map<List<Price>, List<Prices>>([.. db.Price.Where(w => w.ItemId == itemID)]);
    }

    public List<MostRecentPrices> GetRecentPrices()
    {
        return _mapper.Map<List<MostRecentPrice>, List<MostRecentPrices>>([.. db.MostRecentPrice]);
    }

    public ItemDetails GetItemDetail(int itemID)
    {
        DateTime date = DateTime.Today.AddDays(-1);

        var item = db.Item.Where(w => w.ItemId == itemID).FirstOrDefault() ?? new();
        var i = db.Icon.Where(w => w.Id == item.IconId).FirstOrDefault() ?? new();
        var c = db.Category.Where(w => w.Id == item.CategoryId).FirstOrDefault() ?? new();
        var n = db.Name.Where(w => w.Id == item.NameId).FirstOrDefault() ?? new();

        var p = _mapper.Map<List<Price>, List<Prices>>([.. db.Price.Where(w => w.ItemId == item.Id)]);

        return new ItemDetails
        {
            Catagory = item.Category.Name,
            Date = item.ModifiedDate,
            ItemId = item.ItemId,
            LargeIcon = item.Icon.Large,
            Name = item.Name.Value,
            Description = item.Name.Description,
            Prices = p,
            SmallIcon = item.Icon.Small
        };
    }

    public ItemDetails CreateItemDetail(ItemDetails request)
    {
        ItemDetails results = new();

        string createBy = "RickD";
        DateTime date = DateTime.Now;

        var c = db.Category.Where(w => w.Name == request.Catagory).FirstOrDefault();
        if (c == null || c.Id <= 0)
            return results;

        var icon = _mapper.Map<Icon, Icons>(db.Icon.Where(w => w.Small == request.SmallIcon).FirstOrDefault() ?? new());
        if (icon == null || icon.Id <= 0)
            //we have a new item, store it            
            icon = CreateIcon(new Icons()
            {
                Small = request.SmallIcon,
                Large = request.LargeIcon,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        var name = _mapper.Map<Name, Names>(db.Name.Where(w => w.Value == request.Name).FirstOrDefault() ?? new());
        if (name == null || name.Id <= 0)
            name = CreateName(new Names()
            {
                Value = request.Name,
                Description = request.Description,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        var item = _mapper.Map<Item, Items>(db.Item.Where(w => w.ItemId == request.ItemId).FirstOrDefault() ?? new());
        if (item == null || item.Id <= 0)
            item = CreateItem(new Models.Items()
            {
                CategoryId = c.Id,
                IconId = icon.Id,
                ItemID = request.ItemId,
                NameId = name.Id,
                CreatedBy = createBy,
                CreatedDate = date,
                ModifiedBy = createBy,
                ModifiedDate = date
            });

        if (item != null && item.Id > 0)
            CreatePrices(request.Prices, item.Id, createBy, date);

        return GetItemDetail(item?.ItemID ?? 0);
    }

    public ItemDetails UpdateItemDetail(ItemDetails request)
    {
        ItemDetails results = new();

        return results;
    }

    private void CreatePrices(List<Prices>  rawprices, int iD, string createBy, DateTime date)
    {
        List<Prices> prices = [];

        foreach (var p in rawprices)
        {
            var item = db.Price.Where(w => w.ItemId == iD && w.Date == p.Date).FirstOrDefault();
            if (item == null || item.Id <= 0)
            {
                p.ItemId = iD;
                p.CreatedBy = createBy;
                p.CreatedDate = date;
                p.ModifiedBy = createBy;
                p.ModifiedDate = date;
                prices.Add(p);
            }
        }

        using var transaction = db.Database.BeginTransaction();
        db.Price.AddRange(_mapper.Map<List<Prices>, List<Price>>(prices));
        db.SaveChanges();
        db.Database.CommitTransaction();

    }

    public void CreatePrice(Prices price)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Price.Add(_mapper.Map<Prices, Price>(price));
        db.SaveChanges();
        db.Database.CommitTransaction();
    }

    public Categories GetCatagoryByID(int ID)
    {
        return _mapper.Map <Category, Categories>(db.Category.Where(w => w.Id == ID).FirstOrDefault() ?? new());
    }

    public Categories GetCatagoryByName(string Name)
    {
        return _mapper.Map<Category, Categories>(db.Category.Where(w => w.Name == Name).FirstOrDefault() ?? new());
    }

    public List<Categories> GetCatagories()
    {
        return  _mapper.Map<List<Category>, List<Categories>>([.. db.Category.Where(w => !w.Name.StartsWith("Deleted - "))]);
    }

    private Icons GetIcon(string small)
    {
        return _mapper.Map<Icon, Icons>(db.Icon.Where(w => w.Small == small).FirstOrDefault() ?? new Icon());
    }

    private Icons CreateIcon(Icons icon)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Icon.Add(_mapper.Map<Icons, Icon>(icon));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetIcon(icon.Small);
    }


    private Items GetItem(int itemID)
    {
        return _mapper.Map<Item, Items>(db.Item.Where(w => w.ItemId == itemID).FirstOrDefault() ?? new());
    }

    private Items CreateItem(Items item)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Item.Add(_mapper.Map<Items, Item>(item));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetItem(item.ItemID);
    }

    private Names GetName(string value)
    {
        return _mapper.Map<Name, Names>(db.Name.Where(w => w.Value == value).FirstOrDefault() ?? new Name());
    }

    private Names CreateName(Names name)
    {
        using var transaction = db.Database.BeginTransaction();
        db.Name.Add(_mapper.Map<Names, Name>(name));
        db.SaveChanges();
        db.Database.CommitTransaction();

        return GetName(name.Value);
    }

    #endregion

}
