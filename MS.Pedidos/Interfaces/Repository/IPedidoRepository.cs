using MS.Pedidos.Entities;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task<int> Post(Pedido Pedido);
        Task Put();
        Task Remove();
        Task Pacth();
        Task<PedidoDTO> GetPedido();
        Task<IEnumerable<PedidoDTO>> GetAll();
    }
}
