using RssFeed.DTOs.Responses;

namespace RssFeed.Clients.Interfaces
{
    public interface IRssFeedClient
    {
        public Task<List<Publication>> GetFeedNewsAsync(Uri link);
    }
}
