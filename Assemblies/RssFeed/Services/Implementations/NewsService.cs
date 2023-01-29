using RssFeed.Clients.Interfaces;
using RssFeed.DTOs.Requests;
using RssFeed.DTOs.Responses;
using RssFeed.Services.Interfaces;
using RssFeedAPI.DataAccessLayer.Entities;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

namespace RssFeed.Services.Implementations
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IRssFeedClient _rssFeedClient;
        private readonly IFeedRepository _feedRepository;

        public NewsService(IRssFeedClient rssFeedClient, IFeedRepository feedRepository, INewsRepository newsRepository)
        {
            _rssFeedClient = rssFeedClient;
            _feedRepository = feedRepository;
            _newsRepository = newsRepository;
        }

        public async Task AddReadPublicationAsync(AddReadPublicationRequestModel alreadyReadPublication)
        {
            var newPublication = new ReadPublication
            {
                Id = Guid.NewGuid(),
                Link = alreadyReadPublication.PublicationLink
            };
            await _newsRepository.AddReadPublication(newPublication);
        }

        public async Task<List<Publication>> GetAllUnreadNewsPublicationsAsync(DateTimeOffset date)
        {
            var feeds = await _feedRepository.GetAllFeedsAsync();
            List<Publication> allNews = new List<Publication>();
            
              foreach(var feed in feeds)
              {
                var newsFromFeed = await _rssFeedClient.GetFeedNewsAsync(feed.Url, date);
                allNews.AddRange(newsFromFeed);
              }
            return  allNews;
             
        }
    }
}
