using RssFeed.DTOs.Requests;
using RssFeed.DTOs.Responses;
using RssFeedAPI.DataAccessLayer.Entities;

namespace RssFeed.Services.Interfaces
{
    public interface IFeedService
    {
        Task<Feed> AddFeedAsync(CreateFeedRequestModel feedModel);
        Task<GetAllFeedsResponce> GetAllFeedsAsync();
    }
}
