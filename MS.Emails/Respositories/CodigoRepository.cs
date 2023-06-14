using Microsoft.EntityFrameworkCore;
using MS.Emails.Entities;
using MS.Emails.Interfaces;

namespace MS.Emails.Respositories
{
    public class CodigoRepository : ICodigoEmailRepository
    {
        private readonly AppDbContext _context;

        public CodigoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSync(CodigoEmail codigoEmail)
        {
             await _context.CodigosEmail.AddAsync(codigoEmail);
             await _context.SaveChangesAsync();
        }

        public async Task<CodigoEmail> GetByCodigoAsync(string codigo)
        {
             var result = await _context.CodigosEmail.FirstOrDefaultAsync(x => x.Codigo == codigo);

             if(result == null)
                throw new Exception("Código não encontrado ou expirado");


             return result;
            
        }

        public async Task DeleteAsync(string codigo)
        {
            var result = await _context.CodigosEmail.FirstOrDefaultAsync(x => x.Codigo == codigo);

            if(result == null)
                throw new Exception("Código não encontrado ou expirado");

            _context.CodigosEmail.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
}
