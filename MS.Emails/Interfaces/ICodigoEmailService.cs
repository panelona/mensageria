using MS.Emails.Entities;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces
{
    public interface ICodigoEmailService
    {
        Task<string> CadastrarCodigoAsync(EmailRequestDto request);
        string ObterUrlConfirmacaoAsync(string urlBase, string codigo);
        Task EnviarEmailConfirmacaoAsync(string email, string linkConfirmacao);
        Task<bool> EnviarEmailAsync(string toEmail, string subject, string body, string fromEmail, string fromName);

        Task GerarCodigoConfirmacaoAsync(EmailRequestDto email);
        
    }
}
