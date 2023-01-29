using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RssFeedAPI.DataAccessLayer.Entities;

namespace RssFeedAPI.DataAccessLayer.Contexts
{
    public class Context : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _cofiguration;

        public Context(DbContextOptions<Context> options, IConfiguration cofiguration) : base(options)
        {
            _cofiguration = cofiguration;
        }

        public DbSet<Feed> Feeds { get; set; }
        public DbSet<ReadPublication> ReadPublications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_cofiguration.GetConnectionString("DefaultConnection"));
        }

    }
}
