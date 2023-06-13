using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces
{
    public interface IRabbitMqClient
    {
        void EnviaEmailConfirmado(string email);
    }
}
