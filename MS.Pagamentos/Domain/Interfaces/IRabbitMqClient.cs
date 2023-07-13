using MS.Pagamentos.Domain.Entities;

namespace MS.Pagamentos.Domain.Interfaces
{
    public interface IRabbitMqClient
    {
        void EnviaParaPedido(RespostaPagamentoEntitie respostaPagamento);
        void EnviaParaEmail(RespostaPagamentoEntitie respostaPagamento);
    }
}
