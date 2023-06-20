using AutoMapper;
using MS.Emails.Entities;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Profiles
{
    public class CodigoEmailProfile : Profile
    {
        public CodigoEmailProfile()
        {
            CreateMap<EmailRequestDto, CodigoEmail>().ReverseMap();
            CreateMap<EmailResponseDto, CodigoEmail>().ReverseMap();
        }
    }
}
