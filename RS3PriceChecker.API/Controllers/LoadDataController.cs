using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RS3PriceChecker.Database;
using RS3PriceChecker.Repository;
using RS3PriceChecker.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RS3PriceChecker.API.Controllers
{
    [ApiController]
    [Route("api/LoadData")]
    public class LoadDataController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<LoadDataController> _logger;

        private readonly LoadDataService _LoadDataService;
        private readonly IServiceProvider ServiceProvider;

        public LoadDataController(IConfiguration config, ILogger<LoadDataController> logger, LoadDataService loadDataService, IServiceProvider serviceProvider) //, IItemDetailRepository item, RS3PriceCheckerDBContext context)
        {
            _config = config;
            _logger = logger;
            _LoadDataService = loadDataService;
            ServiceProvider = serviceProvider;
        }

        #region Load GE Item Details

        [HttpPost("GetPrices")]
        public string GetPrices(List<int> items)
        {
            var results = _LoadDataService.GetPrices(items);

            return results;
        }

        [HttpGet("LoadAllGEItems")]
        public void LoadAllGEItems()
        {
            _logger.LogInformation("Processing LoadAllGEItems");
            string outputPath = _config.GetValue<string>("AppSettings:OutputPath");

            Task.Run(() => _LoadDataService.LoadAllGEItems(outputPath));
            _logger.LogInformation("Processed LoadAllGEItems");
        }

        #endregion

        #region Load GE Item Detail Files

        [HttpGet("LoadItemFiles")]
        public void LoadItemFiles(string path)
        {
            _LoadDataService.LoadItemFiles(path);
        }

        [HttpGet("LoadItemFilesAsync")]
        public async void LoadItemFilesAsync(string path)
        {
            _logger.LogInformation("Processing LoadItemFilesAsync");
            await using var scope = ServiceProvider.CreateAsyncScope();
            var scopedService = scope.ServiceProvider.GetRequiredService<LoadDataService>();
            await Task.Run(() => scopedService.LoadItemFiles(path));
            _logger.LogInformation("Processed LoadItemFilesAsyncv");
        }

        #endregion

    }

}
