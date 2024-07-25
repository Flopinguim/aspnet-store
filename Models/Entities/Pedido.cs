namespace aspnet_store.Models.Entities
{
    public abstract class Pedido
    {
        public int Id { get; set; }
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public int UsuarioSolicitanteId { get; set; }
        public Usuario UsuarioSolicitante { get; set; }
        public DateTime DataCadastro { get; set; }
        public int? OrdemCompraId { get; set; }
        public OrdemCompra OrdemCompra { get; set; }
    }
}
