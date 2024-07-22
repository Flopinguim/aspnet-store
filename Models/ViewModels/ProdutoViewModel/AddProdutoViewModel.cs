namespace aspnet_store.Models.ViewModels.ProdutoViewModel
{
    public class AddProdutoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoEAN { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int CotaMinima { get; set; }
    }
}
