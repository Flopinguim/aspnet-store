namespace aspnet_store.Models.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public int UsuarioSolicitanteId { get; set; }
        public Usuario UsuarioSolicitante { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
