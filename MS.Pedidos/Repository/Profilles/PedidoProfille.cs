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
            //CreateMap<PedidoDTO, PedidoAtualizaStatusDTO>().ReverseMap();
            CreateMap<PedidoDTO, PedidoEnvio>().ReverseMap();
            CreateMap<PedidoDTO, PedidoPagamento>().ReverseMap();
            CreateMap<PedidoAtualizaStatusDTO, PedidoPagamento>().ReverseMap();
            CreateMap<PedidoAtualizaStatusDTO, PedidoEnvio>().ReverseMap();
            CreateMap<Pedido, PedidoEnvio>().ReverseMap();
            CreateMap<Pedido, PedidoPagamento>().ReverseMap();

        }
    }
}
