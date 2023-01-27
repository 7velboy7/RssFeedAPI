using RssFeed.Clients.Interfaces;
using RssFeed.DTOs.Responses;
using RssFeed.Services.Interfaces;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

namespace RssFeed.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly IRssFeedClient _rssFeedClient;
        private readonly IFeedRepository _feedRepository;

        public NewsService(IRssFeedClient rssFeedClient, IFeedRepository feedRepository)
        {
            _rssFeedClient = rssFeedClient;
            _feedRepository = feedRepository;
        }

        public async Task<List<Publication>> GetAllUnreadNewsPublicationsAsync()
        {
            var feeds = await _feedRepository.GetAllFeedsAsync();
            List<Publication> allNews = new List<Publication>();
            
              foreach(var feed in feeds)
              {
                var newsFromFeed = await _rssFeedClient.GetFeedNewsAsync(feed.Url);
                allNews.AddRange(newsFromFeed);
              }
            return  allNews;
             
        }
    }
}
