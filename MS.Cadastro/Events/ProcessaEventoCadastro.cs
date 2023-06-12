using AutoMapper;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces;
using MS.Cadastro.Interfaces.Services;
using System.Text.Json;

namespace MS.Cadastro.Events
{
    public class ProcessaEventoCadastro : IProcessaEvento
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEventoCadastro(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var _usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();

            var usuarioResponse = JsonSerializer.Deserialize<UsuarioResponse>(mensagem);

            _usuarioService.AlterarStatusAsync(usuarioResponse.Email);
        }
    }
}
