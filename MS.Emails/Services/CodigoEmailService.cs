using System.Security.Cryptography;
using AutoMapper;
using MS.Emails.Entities;
using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Services
{
    public class CodigoEmailService : ICodigoEmailService
    {
        private readonly ICodigoEmailRepository _repository;
        private readonly IMapper _mapper;

        public CodigoEmailService(ICodigoEmailRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CadastrarCodigoAsync(EmailRequestDto request)
        {

            var entity = _mapper.Map<CodigoEmail>(request);


            entity.Codigo = CreateRandomToken();
            entity.GeradoEm = DateTime.Now;
            await _repository.AddSync(entity);
        }
       

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(2));
        }


        
    }
}
