using MS.Emails.Entities;

namespace MS.Emails.Interfaces
{
    public interface ICodigoEmailRepository
    {
        Task AddSync(CodigoEmail codigoEmail);
        Task<CodigoEmail> GetByCodigoAsync(string codigo);

        Task DeleteAsync(string codigo);
    }
}
