namespace MS.Pagamentos.Domain.Interfaces
{
    public interface IProcessaEvento
    {
        Task Processa(string mensagem);
    }
}
