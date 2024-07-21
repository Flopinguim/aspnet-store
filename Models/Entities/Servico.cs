namespace aspnet_store.Models.Entities
{
    public class Servico
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Guid FornecedorId { get; set; }
        public string Descricao { get; set; }
        public TimeSpan PrazoEntrega { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
