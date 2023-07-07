using MS.Pedidos.Entities;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task Post(Pedido Pedido);
        Task Put();
        Task Remove();
        Task Patch();
        Task Patch(StatusPedido pedidoAtualizado, Guid id);
        Task<IEnumerable<PedidoDTO>> GetAll();
        Task<Pedido> GetById(Guid Id);
    }
}
