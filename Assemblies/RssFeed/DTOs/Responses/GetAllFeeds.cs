using RssFeedAPI.DataAccessLayer.Entities;

namespace RssFeed.DTOs.Responses
{
    public class GetAllFeeds
    {
        public List<Feed> FeedResponceList { get; set; }

        public int FeedsCount { get; set; }
        public Uri CrrentFeed { get; set; }
    }
}
