using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Skor> Skorlar { get; set; }

        public DbSet<Oyun> Oyunlar { get; set; }
    }
}
