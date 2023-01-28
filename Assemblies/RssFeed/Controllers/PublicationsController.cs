using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RssFeed.Services.Implementations;
using RssFeed.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> GetAllUnreadPublicationsAsync([Required] DateTimeOffset? date)
        {
            var resultNews = await _newsService.GetAllUnreadNewsPublicationsAsync(date.Value);
            _logger.LogInformation("News were got");
            return Ok(resultNews);
        }


        [HttpPost]
        public async Task<IActionResult> ReadPublicationAsync()
        {
            return Ok();

        }
    }
}
