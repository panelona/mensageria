namespace MS.Pedidos.Entities
{
    public class Pedido
    {
        public Guid Id { get; init; } = new Guid();
        public int NumeroPedido { get; set; }

        public string Item { get; set; }

        public string NumeroCartao { get; set; }

        public string Cep { get; set; }

        public string EmailCliente { get; set; }
    }
}
