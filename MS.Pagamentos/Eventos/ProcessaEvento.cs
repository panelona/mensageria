using MS.Pagamentos.Domain.Entities;
using MS.Pagamentos.Domain.Interfaces;
using System.Text.Json;

namespace MS.Pagamentos.Eventos
{
    public class ProcessaEvento : IProcessaEvento
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEvento(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async Task Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var service = scope.ServiceProvider.GetRequiredService<IPagamentoService>();

            try
            {
                var mensagemRequest = JsonSerializer.Deserialize<DadosPagamentoEntitie>(mensagem);
                if (mensagemRequest != null)
                    await service.AdicionarPagamentoAsync(mensagemRequest);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
