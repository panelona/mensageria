using MS.Cadastro.Contracts;

namespace MS.Cadastro.RabbitMqClient
{
    public interface IRabbitMqClient
    {
        void EnviaParaMsEmail(UsuarioResponse usuario);
    }
}
