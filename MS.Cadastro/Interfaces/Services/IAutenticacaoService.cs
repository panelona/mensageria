using MS.Cadastro.Contracts;

namespace MS.Cadastro.Interfaces.Services
{
    public interface IAutenticacaoService
    {
        Task<AutenticacaoResponse> AutenticarAsync(string email, string senha);
    }
}
