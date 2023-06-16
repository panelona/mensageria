// Ignore Spelling: Criar

using AutoMapper;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.RabbitMqClient;
using System.Text.RegularExpressions;

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

        public async Task AlterarStatusAsync(UsuarioResponse response)
        {
            var entity = await _usuarioRepository.FindAsync(p => p.Email.Equals(response.Email));
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            entity.Status = true;
            await _usuarioRepository.EditAsync(entity);
        }

        public async Task<UsuarioResponse> AtualizarAsync(Guid? id, UsuarioRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioResponse> CriarAsync(UsuarioRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            if (request.Senha.Length < 6) throw new ArgumentException("Senha tem que ser maior que 6 dígitos");

            Regex email = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+.[^@ \t\r\n]+");
            if (!email.IsMatch(request.Email))
            {
                throw new ArgumentException("Email inserido inválido.");
            }

            if (request.Nome.Length < 3) throw new ArgumentException($"Nome{request.Nome} inválido.");

            var usuarioEntity = _mapper.Map<Usuario>(request);
            usuarioEntity.Status = false;
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
