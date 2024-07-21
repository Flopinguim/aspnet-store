namespace aspnet_store.Models.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public string CodigoSolicitacao { get; set; }
        public string CodigoEAN { get; set; }
        public string NomeProduto { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public string DepartamentoSolicitante { get; set; }
        public string UsuarioSolicitante { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
