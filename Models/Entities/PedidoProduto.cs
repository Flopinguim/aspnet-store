namespace aspnet_store.Models.Entities
{
    public class PedidoProduto : Pedido
    {
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
