using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.DepartamentoViewModels
{
    public class AddDepartamentoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }
    }
}
