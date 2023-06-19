using Microsoft.IdentityModel.Tokens;
using MS.Cadastro.Contracts;
using MS.Cadastro.Entity;
using MS.Cadastro.Interfaces.Repositories;
using MS.Cadastro.Interfaces.Services;
using MS.Cadastro.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MS.Cadastro.Services
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private string _tokenJwt;
        private DateTime? _expiracao;
        private readonly IConfiguration _configuration;

        public AutenticacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<AutenticacaoResponse> AutenticarAsync(string email, string senha)
        {
            var entity = await _usuarioRepository.FindAsync(x => x.Email.Equals(email));

            if (!(Criptografia.Encrypt(senha) == entity.Senha))
            {
                throw new ArgumentException("Usuário ou senha incorreta");
            }

            await this.MontaToken(entity);

            return new AutenticacaoResponse
            {
                Token = _tokenJwt,
                DataExpiracao = _expiracao
            };
        }
        private async Task MontaToken(Usuario entity)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, entity.Id.ToString()),
                    new Claim(ClaimTypes.Name, entity.Nome),
                    new Claim(ClaimTypes.Email, entity.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["MS_CADASTRO_JWTSECURITYKEY"])),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _tokenJwt = tokenHandler.WriteToken(token);
            _expiracao = tokenDescriptor.Expires;
        }
    }
}
