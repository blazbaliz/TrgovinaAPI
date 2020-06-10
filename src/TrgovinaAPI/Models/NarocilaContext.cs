using Microsoft.EntityFrameworkCore;

namespace TrgovinaAPI.Models
{
    public class NarocilaContext : DbContext
    {
        public NarocilaContext(DbContextOptions<NarocilaContext> options) : base(options)
        {

        }

        public DbSet<Narocilo> Narocila { get; set; }
    }
}