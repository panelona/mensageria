using MS.Pedidos.Entities;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task Post(Pedido Pedido);
        Task Put();
        Task Remove();
        Task Pacth();
        Task Pacth(Pedido pedidoAtualizado);
        Task<IEnumerable<PedidoDTO>> GetAll();
        Task<Pedido> GetById(Guid Id);
    }
}
