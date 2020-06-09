using Microsoft.EntityFrameworkCore;

namespace TrgovinaAPI.Models
{
    public class IzdelekContext : DbContext
    {
        public IzdelekContext(DbContextOptions<IzdelekContext> options) : base(options)
        {

        }

        public DbSet<Izdelek> Izdelki { get; set;}
    }
}