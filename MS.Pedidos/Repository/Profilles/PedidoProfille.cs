using AutoMapper;
using MS.Pedidos.Entities;
using MS.Pedidos.Repository.DTO;

namespace MS.Pedidos.Repository.Profilles
{
    public class PedidoProfille : Profile
    {
        public PedidoProfille()
        {
            CreateMap<Pedido, PedidoDTO>().ReverseMap();
        }
    }
}
