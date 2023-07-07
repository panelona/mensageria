using MS.Pedidos.Entities;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces.Service
{
    public interface IPedidoService
    {
        Task<int> AddAsync(PedidoDTO Pedido);        
        Task DeleteAsync();
        Task EditAsync(StatusPedido statusNovo, Guid idPedido);
        Task<PedidoDTO> ReadPedidoAsync();
        Task<IEnumerable<PedidoDTO>> ReadAllAsync();
    }
}
