using RssFeed.DTOs.Requests;
using RssFeed.DTOs.Responses;

namespace RssFeed.Services.Interfaces
{
    public interface INewsService
    {
        Task<List<Publication>> GetAllUnreadNewsPublicationsAsync(DateTimeOffset date);
        Task AddReadPublicationAsync(AddReadPublicationRequestModel alreadyReadPublication);
    }
}
