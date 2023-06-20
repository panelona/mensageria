using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces;

public interface ICodigoService
{
    Task gerarCodigoConfirmacaoAsync(EmailRequestDto request);
}