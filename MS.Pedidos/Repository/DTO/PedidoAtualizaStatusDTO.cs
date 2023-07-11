using MS.Pedidos.Entities;

namespace MS.Pedidos.Repository.DTO
{
    public class PedidoAtualizaStatusDTO
    {
        public string EmailCliente { get; set; }
        public StatusPedido statusNovo { get; set; }    
    }
}
