using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using AutoMapper;
using MS.Emails.Entities;
using MS.Emails.Enum;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Services
{
    public class EmailService : IEmailService
    {
        private readonly ICodigoEmailRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        private IRabbitMqClient _rabbitMqClient;
        public EmailService(ICodigoEmailRepository repository, IMapper mapper, IConfiguration configuration, IRabbitMqClient rabbitMqClient)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
            _rabbitMqClient = rabbitMqClient;
       
        }

        public async Task<string> CadastrarCodigoAsync(EmailRequestDto request)
        {

            var entity = _mapper.Map<CodigoEmail>(request);
            
            entity.Codigo = CreateRandomToken();
            entity.GeradoEm = DateTime.Now;
            entity.Id = Guid.NewGuid();
            var response = _mapper.Map<EmailResponseDto>(entity);
            await _repository.AddSync(entity);

            

            return response.Codigo;
        }

        public string ObterUrlConfirmacao(string urlBase, string codigo)
        {
            return $"{urlBase}/api/v1/email/confirmar-email?codigo={codigo}";
        }

        public async Task EnviarEmailConfirmacaoAsync(string email, string linkConfirmacao)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (linkConfirmacao == null) throw new ArgumentNullException(nameof(linkConfirmacao));

            await EnviarEmailAsync(
                email,
                "Confirmação de e-mail",
                $"<p>Seu código de confirmação é: <strong>{linkConfirmacao}</strong></p>",
                _configuration["MS_EMAIL_FROM"],
                _configuration["MS_EMAIL_FROMNAME"]);
        }

        public async Task EnviarEmailPedidoAsync(string email, string numeroPedido, string linkPedido)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (numeroPedido == null) throw new ArgumentNullException(nameof(numeroPedido));
            if (linkPedido == null) throw new ArgumentNullException(nameof(linkPedido));

            await EnviarEmailAsync(
                email,
                "Confirmação de e-mail",
                $"<p>Seu pedido <strong>{numeroPedido}</strong> foi realizado com sucesso.<br/> Acompanhe o status do seu pedido  <a href='{linkPedido}'>aqui</a></p>",
                _configuration["MS_EMAIL_FROM"],
                _configuration["MS_EMAIL_FROMNAME"]);
        }

        public async Task EnviarEmailStatusPagamentoAsync(string email, EnumStatus status)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));

            var body = string.Empty;

            if(status == EnumStatus.PagamentoAutorizado)
                body = $"<p>Seu pagamento foi <strong>Autorizado</strong> já começamos a separar seus produtos</p>";
            else if (status == EnumStatus.PagamentoRecusado)
                body = $"<p>Infelizemente seu pagamento foi <strong>recusado</strong></p>";
            else
                throw new ArgumentException("Status de pagamento inválido");

            await EnviarEmailAsync(
                  email,
                  "Confirmação de e-mail",
                  body,
                  _configuration["MS_EMAIL_FROM"],
                  _configuration["MS_EMAIL_FROMNAME"]);
        }


        public async Task<bool> EnviarEmailAsync(string toEmail, string subject, string body, string fromEmail, string fromName)
        {
            var smtpClient = new SmtpClient(
                 _configuration["MS_SMTP_SERVER"], 
                int.Parse(_configuration["MS_SMTP_PORT"])
                );

            smtpClient.Credentials = new NetworkCredential(
                _configuration["MS_SMTP_USERNAME"],
                _configuration["MS_SMTP_PASSWORD"]
                );

            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task GerarCodigoConfirmacaoAsync(EmailRequestDto request)
        {
            try
            {
              
                var codigo = await CadastrarCodigoAsync(request);
        
                var linkConfirmacao =  ObterUrlConfirmacao(_configuration["MS_EMAIL_URLBASE"], codigo);
        
                await EnviarEmailConfirmacaoAsync(request.Email, linkConfirmacao);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> ConfirmarEmailAsync(string codigo)
        {
            if (codigo == null) throw new ArgumentNullException(nameof(codigo));

            var email = await _repository.GetByCodigoAsync(codigo);

            var request = _mapper.Map<EmailRequestDto>(email);

            _rabbitMqClient.EnviaEmailConfirmado(request);

            return request.Email;
        }

        public string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(2));
        }


        
    }
}
