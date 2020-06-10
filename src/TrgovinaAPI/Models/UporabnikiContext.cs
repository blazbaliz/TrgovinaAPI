using Microsoft.EntityFrameworkCore;

namespace TrgovinaAPI.Models
{
    public class UporabnikiContext : DbContext
    {
        public UporabnikiContext(DbContextOptions<UporabnikiContext> options) : base(options)
        {

        }

        public DbSet<Uporabnik> Uporabniki {get; set; }
    }
}