using aspnet_store.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.UsuarioViewModels
{
    public class AddUsuarioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Matrícula é obrigatória")]
        public string Matricula { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Departamento é obrigatório")]
        public int DepartamentoId { get; set; }

        public IEnumerable<Departamento> Departamentos { get; set; }
    }
}
