using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS3PriceChecker.Services
{
    public interface ILoadDataService
    {
        string GetPrices(List<int> items);

        void LoadAllGEItems(string outputPath);

        void LoadItemFiles(string path);
    }
}
