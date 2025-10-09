using RS3PriceChecker.Entities;
using RS3PriceChecker.Models;
using System.Collections.Generic;

namespace RS3PriceChecker.Mapper
{
    public static class ItemMapper
    {
        public static ItemEntity ModelToEntity(Items m)
        {
            return new ItemEntity()
            {
                ID = m.ID,
                ItemID = m.ItemID,
                CategoryId = m.CategoryId,
                IconId = m.IconId,
                NameId = m.NameId,
                CreatedBy = m.CreatedBy,
                CreatedDate = m.CreatedDate,
                ModifiedBy = m.ModifiedBy,
                ModifiedDate = m.ModifiedDate
            };
        }

        public static List<ItemEntity> ModelToEntityList(List<Items> ms)
        {
            List<ItemEntity> results = new();

            foreach (var m in ms)
            {
                results.Add(ModelToEntity(m));
            }
            return results;
        }

        public static Items EntityToModel(ItemEntity e)
        {
            return new Items()
            {
                ID = e.ID,
                ItemID = e.ItemID,
                CategoryId = e.CategoryId,
                IconId = e.IconId,
                NameId = e.NameId,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }

        public static List<Items> EntityToModelList(List<ItemEntity> es)
        {
            List<Items> results = new();

            foreach (var e in es)
            {
                results.Add(EntityToModel(e));
            }
            return results;
        }

    }
}
