using AutoMapper;
using MS.Pedidos.Interfaces.Repository;
using MS.Pedidos.Interfaces.Service;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private IMapper _mapper;

        public PedidoService(IPedidoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<int> AddAsync(PedidoDTO Pedido)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task EditAsync()
        {
            throw new NotImplementedException();
        }

        public Task EditItemAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PedidoDTO>> ReadAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDTO> ReadPedidoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
