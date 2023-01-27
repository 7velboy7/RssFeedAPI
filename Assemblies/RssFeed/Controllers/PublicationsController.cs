using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RssFeed.Services.Implementations;
using RssFeed.Services.Interfaces;

namespace RssFeed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly ILogger<PublicationsController> _logger;
        public PublicationsController(INewsService feedService, ILogger<PublicationsController> logger)
        {
            _newsService = feedService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUnreadPublications()
        {
            var resultNews = await _newsService.GetAllUnreadNewsPublicationsAsync();
            _logger.LogInformation("News were got");
            return Ok(resultNews);
        }
    }
}
