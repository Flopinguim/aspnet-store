namespace aspnet_store.Models.Entities
{
    public class PedidoServico : Pedido
    {
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public string Observacao { get; set; }
    }
}
