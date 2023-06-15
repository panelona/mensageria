using Microsoft.EntityFrameworkCore;

namespace MS.Cadastro.Interfaces.Repositories
{
    public interface ITransientDbContextFactory<TContext> where TContext : DbContext
    {
        TContext CreateDbContext();
    }
}
