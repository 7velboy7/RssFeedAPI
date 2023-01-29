using RssFeedAPI.DataAccessLayer.Entities;

namespace RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces
{
    public interface INewsRepository
    {
        Task<IEnumerable<ReadPublication>> GetAllReadPublicationsAsync();
        Task AddReadPublication(ReadPublication publicationLink);
    }
}
