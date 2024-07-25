namespace aspnet_store.Models.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoEAN { get; set; }
        public decimal PrecoUnitario { get; set; }
        public int Estoque { get; set; }
        public string Fabricante { get; set; }
    }
}
