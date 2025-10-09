using RS3PriceChecker.Entities;
using RS3PriceChecker.Models;
using System.Collections.Generic;

namespace RS3PriceChecker.Mapper
{
    public static class PriceMapper
    {
        public static PriceEntity ModelToEntity(Prices m)
        {
            return new PriceEntity()
            {
                ID = m.ID,
                Amount = m.Amount,
                Date = m.Date,
                CreatedBy = m.CreatedBy,
                CreatedDate = m.CreatedDate,
                ModifiedBy = m.ModifiedBy,
                ModifiedDate = m.ModifiedDate
            };
        }

        public static List<PriceEntity> ModelToEntityList(List<Prices> ms)
        {
            List<PriceEntity> results = new();

            foreach (var m in ms)
            {
                results.Add(ModelToEntity(m));
            }
            return results;
        }

        public static Prices EntityToModel(PriceEntity e)
        {
            return new Prices()
            {
                ID = e.ID,
                Amount = e.Amount,
                Date = e.Date,
                CreatedBy = e.CreatedBy,
                CreatedDate = e.CreatedDate,
                ModifiedBy = e.ModifiedBy,
                ModifiedDate = e.ModifiedDate
            };
        }

        public static List<Prices> EntityToModelList(List<PriceEntity> es)
        {
            List<Prices> results = new();

            foreach (var e in es)
            {
                results.Add(EntityToModel(e));
            }
            return results;
        }
    }
}
