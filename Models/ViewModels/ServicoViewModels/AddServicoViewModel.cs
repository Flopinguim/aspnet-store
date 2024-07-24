using aspnet_store.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.ServicoViewModels
{
    public class AddServicoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Fornecedor é obrigatório")]
        public int FornecedorId { get; set; }

        public string FornecedorNome { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Data Inicial é obrigatório")]

        public DateTime DataInicial { get; set; }
        
        [Required(ErrorMessage = "Data Final é obrigatório")]
        public DateTime DataFinal { get; set; }

        public int PrazoEntrega { get; set; }
    }
}
