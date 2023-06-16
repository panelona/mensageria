using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;

namespace MS.Cadastro.Repositories
{
    public class TransientDbContextFactory : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public TransientDbContextFactory(DbContextOptions<TransientDbContextFactory> options) : base(options)
        {

        }
    }
}
