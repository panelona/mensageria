using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using System.Linq.Expressions;

namespace MS.Cadastro.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ITransientDbContextFactory<AppDbContext> _context;

        public UsuarioRepository(ITransientDbContextFactory<AppDbContext> context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            using (var context = _context.CreateDbContext())
            {
                await context.AddAsync(usuario);
                await context.SaveChangesAsync();
            }
        }

        public async Task EditAsync(Usuario usuario)
        {
            using (var context = _context.CreateDbContext())
            {
                context.Update(usuario);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> FindAsNoTrackingAsync(Expression<Func<Usuario, bool>> expression)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Set<Usuario>().AsNoTracking().FirstOrDefaultAsync(expression);
            }
        }

        public async Task<Usuario> FindAsync(Guid id)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Set<Usuario>().FindAsync(id);
            }
        }

        public async Task<Usuario> FindAsync(Expression<Func<Usuario, bool>> expression)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Set<Usuario>().FirstOrDefaultAsync(expression);
            }
        }

        public async Task<IEnumerable<Usuario>> ListAsync()
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Set<Usuario>().ToListAsync();
            }
        }

        public async Task<IEnumerable<Usuario>> ListAsync(Expression<Func<Usuario, bool>> expression)
        {
            using (var context = _context.CreateDbContext())
            {
                return await context.Set<Usuario>().Where(expression).ToListAsync();
            }
        }

        public async Task RemoveAsync(Usuario usuario)
        {
            using (var context = _context.CreateDbContext())
            {
                context.Set<Usuario>().Remove(usuario);
                await context.SaveChangesAsync();
            }
        }
    }
}
