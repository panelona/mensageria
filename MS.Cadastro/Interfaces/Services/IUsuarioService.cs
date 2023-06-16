using MS.Cadastro.Contracts;

namespace MS.Cadastro.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> CriarAsync(UsuarioRequest request);
        Task<UsuarioResponse> AtualizarAsync(Guid? id, UsuarioRequest request);
        Task DeletarAsync(Guid id);
        Task<UsuarioResponse> ObterPorIdAsync(Guid id);
        Task<IEnumerable<UsuarioResponse>> ObterTodosAsync();
        Task AlterarStatusAsync(UsuarioResponse email);
    }
}