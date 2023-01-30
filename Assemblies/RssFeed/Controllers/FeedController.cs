using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RssFeed.DTOs.Requests;
using RssFeed.Services.Interfaces;

namespace RssFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IFeedService _feedService;
        private readonly ILogger<FeedController> _logger;

        public FeedController(IFeedService feedService, ILogger<FeedController> logger)
        {
            _feedService = feedService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedAsync(CreateFeedRequestModel feedModel) 
        {
            try
            {
                var resultFedd = await _feedService.AddFeedAsync(feedModel);
                _logger.LogInformation("feed was added");
                return StatusCode(StatusCodes.Status201Created, resultFedd);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to add a new feed '{feedModel.Link}' for user '{HttpContext.User.Identity.Name}'");
                return StatusCode(StatusCodes.Status500InternalServerError, "For more information check log files.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedsAsync()
        {
            try
            {
                var resultFeeds = await _feedService.GetAllFeedsAsync();
                _logger.LogInformation("Feeds were added");
                return Ok(resultFeeds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get feeds for user '{HttpContext.User.Identity.Name}'");
                return StatusCode(StatusCodes.Status500InternalServerError, "For more information check log files.");
            }
           
        }
    }
}
