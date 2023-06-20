using Microsoft.EntityFrameworkCore;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Services;

namespace MS.Emails.Respositories
{
    public class CodigoRepository : ICodigoEmailRepository
    {
        private readonly IDbContextFactory<TransientDbContextFactory> _context;

        public CodigoRepository(IDbContextFactory<TransientDbContextFactory> context)
        {
            _context = context;
        }

        public async Task AddSync(CodigoEmail codigoEmail)
        {
            using (var context = _context.CreateDbContext())
            {
                await context.CodigosEmail.AddAsync(codigoEmail);
                await context.SaveChangesAsync();
            }
             
        }

        public async Task<CodigoEmail> GetByCodigoAsync(string codigo)
        {
            using (var context = _context.CreateDbContext())
            {
                var result = await context.CodigosEmail.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if(result == null)
                    throw new Exception("Código não encontrado ou expirado");


                return result;
            }
            
        }

        public async Task DeleteAsync(string codigo)
        {
            using (var context = _context.CreateDbContext())
            {
                var result = await context.CodigosEmail.FirstOrDefaultAsync(x => x.Codigo == codigo);

                if(result == null)
                    throw new Exception("Código não encontrado ou expirado");

                context.CodigosEmail.Remove(result);
                await context.SaveChangesAsync();
            }
            
            
        }
    }
}
