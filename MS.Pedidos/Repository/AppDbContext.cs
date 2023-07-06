using Microsoft.EntityFrameworkCore;
using MS.Pedidos.Entities;

namespace MS.Pedidos.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
