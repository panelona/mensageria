using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces
{
    public interface IRabbitMqClient
    {
        void EnviaParaPagamento(PedidoPagamento pedido);
        void EnviaParaEnvios(PedidoEnvio pedido);
    }
}
