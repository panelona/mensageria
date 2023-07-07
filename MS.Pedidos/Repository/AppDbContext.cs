using Microsoft.EntityFrameworkCore;
using MS.Pedidos.Entities;

namespace MS.Pedidos.Repository
{
    public class AppDbContext : DbContext
    {   
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(new PedidoEntityMap().Configure);
        }
    }
   
}
