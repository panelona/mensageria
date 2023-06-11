// Ignore Spelling: Criar

using AutoMapper;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.RabbitMqClient;

namespace MS.Cadastro.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IRabbitMqClient _rabbitMqClient;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper, IRabbitMqClient rabbitMqClient)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _rabbitMqClient = rabbitMqClient;
        }

        public async Task<UsuarioResponse> AtualizarAsync(Guid? id, UsuarioRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioResponse> CriarAsync(UsuarioRequest request)
        {
            var usuarioEntity = _mapper.Map<Usuario>(request);
            await _usuarioRepository.AddAsync(usuarioEntity);

            var usuarioResponse = _mapper.Map<UsuarioResponse>(usuarioEntity);

            _rabbitMqClient.EnviaParaMsEmail(usuarioResponse);

            return _mapper.Map<UsuarioResponse>(usuarioEntity);
        }

        public Task DeletarAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioResponse> ObterPorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioResponse>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
