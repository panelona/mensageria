using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MS.Emails.Interfaces;
using MS.Emails.Respositories;

namespace MS.Emails.Services;

public class TransientDbContextFactory :  ITransientDbContextFactory<AppDbContext>
{
    private readonly IConfiguration _configuration;

    public TransientDbContextFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext CreateDbContext()
    {
        var connectionString = _configuration["MS_EMAIL_CONNSTRING"];
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        
        return new AppDbContext(optionsBuilder.Options);
    }

    
}