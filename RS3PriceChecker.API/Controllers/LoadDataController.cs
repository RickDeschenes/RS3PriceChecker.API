using Microsoft.AspNetCore.Mvc;
using RS3PriceChecker.Services.Services;

namespace RS3PriceChecker.API.Controllers
{
    [ApiController]
    [Route("api/LoadData")]
    public class LoadDataController(IConfiguration config, ILogger<LoadDataController> logger, LoadDataService loadDataService, IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IConfiguration _config = config;
        private readonly ILogger<LoadDataController> _logger = logger;

        private readonly LoadDataService _LoadDataService = loadDataService;
        private readonly IServiceProvider ServiceProvider = serviceProvider;

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
            string outputPath = _config.GetValue<string>("AppSettings:OutputPath") ?? string.Empty;

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
