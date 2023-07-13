using AutoMapper;
using MS.Pedidos.Entities;
using MS.Pedidos.Interfaces;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository.DTO;
using System.Text.Json;

namespace MS.Pedidos.Events
{
    public class ProcessaEvento : IProcessaEvento
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

        public async Task Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IPedidoService>();


            var mensagemResponse = JsonSerializer.Deserialize<PedidoAtualizaStatusDTO>(mensagem);

            if (mensagemResponse != null)
                await service.EditAsync(mensagemResponse);
        }
    }
}
