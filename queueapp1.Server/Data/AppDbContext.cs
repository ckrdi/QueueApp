using Microsoft.EntityFrameworkCore;
using queueapp1.Server.Models;

namespace queueapp1.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }
}
