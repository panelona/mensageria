using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Pedidos.Entities;

namespace MS.Pedidos.Repository
{
    public class TransientDbContextFactory : DbContext
    {
        public TransientDbContextFactory(DbContextOptions<TransientDbContextFactory> options) : base(options) { }
        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PedidoEntityMap());
        }

    }
}
