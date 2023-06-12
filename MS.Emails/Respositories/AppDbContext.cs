using Microsoft.EntityFrameworkCore;
using MS.Emails.Entities;

namespace MS.Emails.Respositories
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<CodigoEmail> CodigosEmail { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}
