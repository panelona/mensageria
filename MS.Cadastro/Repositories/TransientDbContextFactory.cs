using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Interfaces.Repositories;

namespace MS.Cadastro.Repositories
{
    public class TransientDbContextFactory : ITransientDbContextFactory<AppDbContext>
    {
        private readonly IConfiguration _configuration;

        public TransientDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AppDbContext CreateDbContext()
        {
            var connectionString = _configuration["MS_CADASTRO_CONNSTRING"];
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
