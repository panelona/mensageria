using MS.Emails.Entities;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces
{
    public interface ICodigoEmailService
    {
        Task<string> CadastrarCodigoAsync(EmailRequestDto request);
        string ObterUrlConfirmacaoAsync(string urlBase, string codigo);
        
    }
}
