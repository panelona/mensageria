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

        public async void Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var repository = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            
            var email = JsonSerializer.Deserialize<EmailRequestDto>(mensagem);

            //service.gerarCodigoConfirmacaoAsync(email);


            //var entity = _mapper.Map<CodigoEmail>(mensagem);

            var entity = new CodigoEmail
            {
                Codigo = service.CreateRandomToken(),
                Email = email.Email,
                GeradoEm = DateTime.Now
            };


            try
            {
                await repository.AddAsync(entity);
                await repository.SaveChangesAsync();

                var linkConfirmacao = service.ObterUrlConfirmacao(_configuration["MS_EMAIL_URLBASE"], entity.Codigo);


                await service.EnviarEmailConfirmacaoAsync(email.Email, linkConfirmacao);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }



            
        }
    }
}
