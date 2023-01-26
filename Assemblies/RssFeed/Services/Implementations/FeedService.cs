using RssFeed.DTOs.Requests;
using RssFeed.DTOs.Responses;
using RssFeed.Services.Interfaces;
using RssFeedAPI.DataAccessLayer.Entities;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryImplementations;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

namespace RssFeed.Services.Implementations
{
    public class FeedService : IFeedService
    {
        IFeedRepository _feedRepository;

        public FeedService(IFeedRepository repository)
        {
            _feedRepository = repository;
        }

        public async Task<Feed> AddFeedAsync(CreateFeedRequestModel feedModel)
        {
            var newdFeed = new Feed()
            {
                Id = Guid.NewGuid(),
                Url = feedModel.Link
            };
            await _feedRepository.AddFeedAsync(newdFeed);
            return newdFeed;
        }

        public async Task<GetAllFeeds> GetAllFeedsAsync(int feedModel)
        {
            var allFeeds = await _feedRepository.GetAllFeedsAsync();
            return (GetAllFeeds)allFeeds ;
        }
    }
}
