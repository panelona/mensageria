using System.Text.Json;
using AutoMapper;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Respositories;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Events
{
    public class ProcessaEvento: IProcessaEvento
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        private IConfiguration _configuration;

        public ProcessaEvento(IMapper mapper, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
            _configuration = configuration;
        }

        public async Task EnviaEmailConfirmacao(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IEmailService>();
            
            
            var mensagemResponse = JsonSerializer.Deserialize<EmailRequestDto>(mensagem);

            if (mensagemResponse.Email == null)
                throw new Exception("Email não informado");
            
            
            await service.GerarCodigoConfirmacaoAsync(mensagemResponse);
            



         }

        public Task EnviaEmailPedidoRealizado(string mensagem)
        {
            throw new NotImplementedException();
        }

        public Task EnviaEmailStatusPagamento(string mensagem)
        {
            throw new NotImplementedException();
        }
    }
}
