using MS.Emails.Interfaces;
using MS.Emails.Respositories.Dto;

namespace MS.Emails.Services;

public class CodigoService : ICodigoService
{
    private readonly IEmailService _service;
    private readonly IConfiguration _configuration;
    public CodigoService(IEmailService service, IConfiguration configuration)
    {
        _service = service;
        _configuration = configuration;
    }
    
    public async Task gerarCodigoConfirmacaoAsync(EmailRequestDto request)
    {
        
                    try
                    {
                        
                        var codigo = await _service.CadastrarCodigoAsync(request);
        
                        var linkConfirmacao =  _service.ObterUrlConfirmacao(_configuration["MS_EMAIL_URLBASE"], codigo);
        
                        await _service.EnviarEmailConfirmacaoAsync(request.Email, linkConfirmacao);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    
                    
                    
                    
                    
    }
}