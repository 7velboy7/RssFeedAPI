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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_cofiguration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Feed> Feeds { get; set; }

    }
}
