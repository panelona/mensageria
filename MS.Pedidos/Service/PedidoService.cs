using AutoMapper;
using MS.Pedidos.Entities;
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

        public async Task<int> AddAsync(PedidoDTO pedido)
        {
            var entidadeMapeada = _mapper.Map<Pedido>(pedido);
            entidadeMapeada.NumeroPedido += 1;
            entidadeMapeada.StatusPedido = StatusPedido.AGUARDANDO;
            entidadeMapeada.Id = Guid.NewGuid();
            
            await _repository.Post(entidadeMapeada);

            return entidadeMapeada.NumeroPedido;
        }

        public async Task EditAsync(StatusPedido statusNovo, Guid idPedido)
        {
            var pedidoEncontrado = await _repository.GetById(idPedido);
            pedidoEncontrado.StatusPedido = statusNovo;
            await _repository.Pacth(pedidoEncontrado);
        }
        public Task DeleteAsync()
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
