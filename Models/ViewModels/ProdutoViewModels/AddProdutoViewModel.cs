using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.ProdutoViewModels
{
    public class AddProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
        public string CodigoEAN { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Estoque é obrigatório")]
        public int Estoque { get; set; }

        [Required(ErrorMessage = "Fabricante é obrigatório")]
        public string Fabricante { get; set; }
    }
}
