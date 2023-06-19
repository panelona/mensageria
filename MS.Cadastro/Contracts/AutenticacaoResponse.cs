namespace MS.Cadastro.Contracts
{
    public class AutenticacaoResponse
    {
        public string Token { get; set; }
        public DateTime? DataExpiracao { get; set; }
    }
}
