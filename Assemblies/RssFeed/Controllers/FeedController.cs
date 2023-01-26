using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RssFeed.DTOs.Requests;
using RssFeed.Services.Interfaces;

namespace RssFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;
        private readonly ILogger<FeedController> _logger;

        [HttpPost]
        public async Task<IActionResult> AddFeedAsync(CreateFeedRequestModel feedModel) 
        {
            try
            {
                var resultFedd = await _feedService.AddFeedAsync(feedModel);
                _logger.LogInformation("feed was added");
                return Ok(resultFedd);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedsAsync(int countOfFeeds)
        {
            var resultFeeds = await _feedService.GetAllFeedsAsync(countOfFeeds);
            _logger.LogInformation("Feeds were added");
            return Ok(resultFeeds);
        }
    }
}
