using aspnet_store.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.PedidoViewModels
{
    public class AddPedidoViewModel
    {
        [Required(ErrorMessage = "Tipo de Pedido é obrigatório.")]
        public string TipoPedido { get; set; }

        [Required(ErrorMessage = "Departamento é obrigatório.")]
        public int DepartamentoId { get; set; }

        [Required(ErrorMessage = "Usuário solicitante é obrigatório.")]
        public int UsuarioSolicitanteId { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // PedidoProduto
        public int? ProdutoId { get; set; }
        public int? Quantidade { get; set; }

        // PedidoServico
        public int? ServicoId { get; set; }
        public int? FornecedorId { get; set; }
        public string Observacao { get; set; }

        // Dropdowns
        public List<Departamento> Departamentos { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<Servico> Servicos { get; set; }
        public List<Fornecedor> Fornecedores { get; set; }
    }
}