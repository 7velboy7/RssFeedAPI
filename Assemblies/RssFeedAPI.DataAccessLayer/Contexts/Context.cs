using Microsoft.EntityFrameworkCore;
using RssFeedAPI.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssFeedAPI.DataAccessLayer.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Feed> feeds { get; set; }

    }
}
