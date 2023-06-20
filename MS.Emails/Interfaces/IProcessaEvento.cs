namespace MS.Emails.Interfaces
{
    public interface IProcessaEvento
    {
        Task Processa(string mensagem);
    }
}
