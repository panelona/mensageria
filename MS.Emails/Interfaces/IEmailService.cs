using MS.Emails.Entities;
using MS.Emails.Enum;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Interfaces
{
    public interface IEmailService
    {
        Task<string> CadastrarCodigoAsync(EmailRequestDto request);
        string ObterUrlConfirmacao(string urlBase, string codigo);
        Task EnviarEmailConfirmacaoAsync(string email, string linkConfirmacao);
        Task EnviarEmailPedidoAsync(string email, string numeroPedido, string linkPedido);
        Task EnviarEmailStatusPagamentoAsync(string email, EnumStatus status);
        Task<bool> EnviarEmailAsync(string toEmail, string subject, string body, string fromEmail, string fromName);

        Task GerarCodigoConfirmacaoAsync(EmailRequestDto email);

        Task<string> ConfirmarEmailAsync(string codigo);

        string CreateRandomToken();
    }
}
