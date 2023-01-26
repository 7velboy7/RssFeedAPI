using Microsoft.EntityFrameworkCore;
using RssFeedAPI.DataAccessLayer.Contexts;
using RssFeedAPI.DataAccessLayer.Entities;
using RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.feeds.ToListAsync();
        }
    }
}
