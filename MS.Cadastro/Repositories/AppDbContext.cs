using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Entity;

namespace MS.Cadastro.Repositories
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(prop => prop.Email).IsUnique();
        }

    }
}
