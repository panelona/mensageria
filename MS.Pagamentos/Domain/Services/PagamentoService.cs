using MS.Pagamentos.Domain.Entities;
using MS.Pagamentos.Domain.Enums;
using MS.Pagamentos.Domain.Interfaces;

namespace MS.Pagamentos.Domain.Services
{
    public class PagamentoService: IPagamentoService
    {
        private readonly IRabbitMqClient _client;
        public PagamentoService(IRabbitMqClient client) 
        {
            _client = client;
        }
        public async Task AdicionarPagamentoAsync(DadosPagamentoEntitie entity)
        {
            var resposta = new RespostaPagamentoEntitie() { Email = entity.Email, Status = StatusPagamento.Aprovado };
            var isValid = VerificaCartaoValido(entity.NumCartao);
            if (!isValid)
            {
                resposta.Status = StatusPagamento.Reprovado;
            }
            _client.EnviaParaEmail(resposta);
            _client.EnviaParaPedido(resposta);
        }
        
        public static bool VerificaCartaoValido(string numCartao)
        {
            Random rnd = new Random();
            var result = rnd.Next(10);
                return result % 2 == 0;
        }
    }
}
