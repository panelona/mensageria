using System.Linq.Expressions;
using MS.Cadastro.Entity;

namespace MS.Cadastro.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> FindAsync(Guid id);
        Task<Usuario> FindAsync(Expression<Func<Usuario, bool>> expression);
        Task<Usuario> FindAsNoTrackingAsync(Expression<Func<Usuario, bool>> expression);
        Task<IEnumerable<Usuario>> ListAsync();
        Task<IEnumerable<Usuario>> ListAsync(Expression<Func<Usuario, bool>> expression);
        Task AddAsync(Usuario usuario);
        Task RemoveAsync(Usuario usuario);
        Task EditAsync(Usuario usuario);
    }
}
