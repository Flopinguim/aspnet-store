using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.FornecedorViewModels
{
    public class AddFornecedorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Endereco é obrigatório")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "CNPJ é obrigatório")]
        public string CNPJ { get; set; }

        public string? InscricaoEstadual { get; set; }

        public string? InscricaoMunicipal { get; set; }
    }
}
