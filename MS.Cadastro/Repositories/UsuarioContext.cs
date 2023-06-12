using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Entity;

namespace MS.Cadastro.Repositories
{
    public class UsuarioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasIndex(prop => prop.Email).IsUnique();
        }

    }
}
