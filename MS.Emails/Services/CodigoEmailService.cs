using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Services
{
    public class CodigoEmailService : ICodigoEmailService
    {
        private readonly ICodigoEmailRepository _repository;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public CodigoEmailService(ICodigoEmailRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> CadastrarCodigoAsync(EmailRequestDto request)
        {

            var entity = _mapper.Map<CodigoEmail>(request);


            entity.Codigo = CreateRandomToken();
            entity.GeradoEm = DateTime.Now;
            await _repository.AddSync(entity);

            var response = _mapper.Map<EmailResponseDto>(entity);

            return response.Codigo;
        }

        public string ObterUrlConfirmacaoAsync(string urlBase, string codigo)
        {
            return $"{urlBase}/confirmaemail?token={codigo}";
        }

        public async Task EnviarEmailConfirmacaoAsync(string email, string linkConfirmacao)
        {
            if (email == null) throw new ArgumentNullException(nameof(email));
            if (linkConfirmacao == null) throw new ArgumentNullException(nameof(linkConfirmacao));

            await EnviarEmailAsync(
                email,
                "Confirmação de e-mail",
                $"<p>Seu código de confirmação é: <strong>{linkConfirmacao}</strong></p>",
                "dev@wilsonsantos.com.br",
                "Microsserviço de mensageria");
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

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(2));
        }


        
    }
}
