using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces.Service
{
    public interface IPedidoService
    {
        Task<int> AddAsync(PedidoDTO Pedido);
        Task EditAsync();
        Task DeleteAsync();
        Task EditItemAsync();
        Task<PedidoDTO> ReadPedidoAsync();
        Task<IEnumerable<PedidoDTO>> ReadAllAsync();
    }
}
