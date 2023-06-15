using Microsoft.EntityFrameworkCore;

namespace MS.Emails.Interfaces;

public interface ITransientDbContextFactory<TContext> where TContext : DbContext
{
    TContext CreateDbContext();
}