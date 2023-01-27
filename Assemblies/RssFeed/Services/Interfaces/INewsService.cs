using RssFeed.DTOs.Responses;

namespace RssFeed.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<Publication>> GetAllUnreadNewsPublicationsAsync();
    }
}
