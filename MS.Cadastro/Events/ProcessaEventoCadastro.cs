using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.Repositories;
using System.Text.Json;

namespace MS.Cadastro.Events
{
    public class ProcessaEventoCadastro : IProcessaEventoCadastro
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProcessaEventoCadastro(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public async void Processa(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var _usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();

            var msgResponse = JsonSerializer.Deserialize<UsuarioResponse>(mensagem);

            await _usuarioService.AlterarStatusAsync(msgResponse);
        }
    }
}
