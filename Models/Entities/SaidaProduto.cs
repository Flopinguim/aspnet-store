namespace aspnet_store.Models.Entities
{
    public class SaidaProduto
    {
        public int Id { get; set; }
        public int OrdemCompraId { get; set; }
        public OrdemCompra OrdemCompra { get; set; }
        public int Quantidade { get; set; }
        public string Usuario { get; set; }
        public string DepartamentoSolicitante { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
