using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _cofiguration;

        public Context(DbContextOptions<Context> options, IConfiguration cofiguration) : base(options)
        {
            _cofiguration = cofiguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_cofiguration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Feed> Feeds { get; set; }

    }
}
