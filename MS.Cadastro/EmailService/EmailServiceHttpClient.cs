using MS.Cadastro.Contracts;
using System.Text;
using System.Text.Json;

namespace MS.Cadastro.EmailService
{
    public class EmailServiceHttpClient : IEmailServiceHttpClient
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;

        public EmailServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async void EnviaUsuarioParaEmailService(UsuarioResponse usuario)
        {
            var conteudoHttp = new StringContent
                (
                    JsonSerializer.Serialize(usuario),
                    Encoding.UTF8,
                    "application/json"
                );

            await _httpClient.PostAsync(_configuration["EmailService"], conteudoHttp);
        }
    }
}
