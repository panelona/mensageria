using MS.Cadastro.Contracts;

namespace MS.Cadastro.EmailService
{
    public interface IEmailServiceHttpClient
    {
        public void EnviaUsuarioParaEmailService(UsuarioResponse usuario);
    }
}
