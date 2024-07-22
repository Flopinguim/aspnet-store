namespace aspnet_store.Models.Entities
{
    public class OrdemCompra
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
