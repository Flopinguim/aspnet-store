namespace aspnet_store.Models.Entities
{
    public class EntradaProduto
    {
        public int Id { get; set; }
        public int OrdemCompraId { get; set; }
        public OrdemCompra OrdemCompra { get; set; }
        public string NotaFiscal { get; set; }
        public int Quantidade { get; set; }
        public string Deposito { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
