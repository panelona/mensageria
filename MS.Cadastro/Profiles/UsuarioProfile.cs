using AutoMapper;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;

namespace MS.Cadastro.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<Usuario, UsuarioRequest>().ReverseMap();   
            CreateMap<Usuario, UsuarioResponse>().ReverseMap();   
        }
    }
}
