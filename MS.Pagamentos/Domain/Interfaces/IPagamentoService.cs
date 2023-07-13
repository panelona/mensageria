using MS.Pagamentos.Domain.Entities;

namespace MS.Pagamentos.Domain.Interfaces
{
    public interface IPagamentoService
    {
        Task AdicionarPagamentoAsync(DadosPagamentoEntitie entity);
    }
}
