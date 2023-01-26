using RssFeedAPI.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssFeedAPI.DataAccessLayer.Repositories.RepositoryInterfaces
{
    public interface IFeedRepository
    {
        Task AddFeedAsync(Feed feed);
        Task<IEnumerable<Feed>> GetAllFeedsAsync();
    }
}
