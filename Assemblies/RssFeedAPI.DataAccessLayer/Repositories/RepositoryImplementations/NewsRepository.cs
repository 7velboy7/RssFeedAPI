using Microsoft.EntityFrameworkCore;
using RssFeedAPI.DataAccessLayer.Contexts;
using RssFeedAPI.DataAccessLayer.Entities;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

namespace RssFeedAPI.DataAccessLayer.Repositories.RepositoryImplementations
{
    public class NewsRepository : INewsRepository
    {
        private readonly Context _context;
        public NewsRepository(Context context)
        {
            _context = context;
        }

        public async Task AddReadPublication(ReadPublication publication)
        {
            await _context.ReadPublications.AddAsync(publication);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReadPublication>> GetAllReadPublicationsAsync()
        {
            return await _context.ReadPublications.ToListAsync();
        }

    }
}
