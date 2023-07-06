using MS.Pedidos.Entities;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        public Task<IEnumerable<PedidoDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> GetPedido()
        {
            throw new NotImplementedException();
        }

        public Task Pacth()
        {
            throw new NotImplementedException();
        }

        public Task<int> Post(Pedido Pedido)
        {
            throw new NotImplementedException();
        }

        public Task Put()
        {
            throw new NotImplementedException();
        }

        public Task Remove()
        {
            throw new NotImplementedException();
        }
    }
}
