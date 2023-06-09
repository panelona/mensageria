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
    }
}
