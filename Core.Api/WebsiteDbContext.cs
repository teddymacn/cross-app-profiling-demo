
using Core.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Api
{
    public class WebsiteDbContext : DbContext
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
    }
}