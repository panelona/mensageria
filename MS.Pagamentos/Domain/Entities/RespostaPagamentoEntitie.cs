using MS.Pagamentos.Domain.Enums;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace MS.Pagamentos.Domain.Entities
{
    public class RespostaPagamentoEntitie
    {
        public string Email { get; set; }

        public StatusPagamento Status { get; set; }
    }
}
