namespace MS.Emails.Interfaces
{
    public interface IProcessaEvento
    {
        Task EnviaEmailConfirmacao(string mensagem);
        Task EnviaEmailPedidoRealizado(string mensagem);
        Task EnviaEmailStatusPagamento(string mensagem);
    }
}
