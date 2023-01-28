using Microsoft.EntityFrameworkCore;
using RssFeedAPI.DataAccessLayer.Contexts;
using RssFeedAPI.DataAccessLayer.Entities;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;

namespace RssFeedAPI.DataAccessLayer.Repositories.RepositoryImplementations
{
    public class FeedRepository : IFeedRepository
    {
        private Context _context;

        public FeedRepository(Context context)
        {
            _context = context;
        }

        public async Task AddFeedAsync(Feed feed)
        {
            await _context.AddAsync(feed);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Feed>> GetAllFeedsAsync()
        {
            return await _context.Feeds.ToListAsync();
        }
    }
}
