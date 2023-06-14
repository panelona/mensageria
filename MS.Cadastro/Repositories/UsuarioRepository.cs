using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MS.Cadastro.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuarioContext _context;

        public UsuarioRepository(UsuarioContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Set<Usuario>().AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Usuario usuario)
        {
            _context.Set<Usuario>().Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> FindAsNoTrackingAsync(Expression<Func<Usuario, bool>> expression)
        {
            return await _context.Set<Usuario>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<Usuario> FindAsync(Guid id)
        {
            return await _context.Set<Usuario>().FindAsync(id);
        }

        public async Task<Usuario> FindAsync(Expression<Func<Usuario, bool>> expression)
        {
            try
            {
                return await _context.Set<Usuario>().FirstOrDefaultAsync(expression);
            }
            catch (Exception)
            {

                throw;
            } 
           
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            return await _context.Set<Usuario>().ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> ListAsync(Expression<Func<Usuario, bool>> expression)
        {
            return await _context.Set<Usuario>().Where(expression).ToListAsync();
        }

        public async Task RemoveAsync(Usuario usuario)
        {
            _context.Set<Usuario>().Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
