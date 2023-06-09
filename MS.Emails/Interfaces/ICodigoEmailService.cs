using MS.Emails.Entities;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces
{
    public interface ICodigoEmailService
    {
        Task CadastrarCodigoAsync(EmailRequestDto request);
        
    }
}
