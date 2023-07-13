namespace MS.Pedidos.Interfaces
{
    public interface IProcessaEvento
    {
        Task Processa(string mensagem);
    }
}
