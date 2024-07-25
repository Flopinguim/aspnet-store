using aspnet_store.Models.Entities;
using System;
using System.Collections.Generic;

namespace aspnet_store.Models.ViewModels.EstoqueViewModels
{
    public class SaidaProdutoViewModel
    {
        public int OrdemCompraId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
        public string Usuario { get; set; }
        public string DepartamentoSolicitante { get; set; }

        public List<Usuario> Usuarios { get; set; }
        public List<Departamento> Departamentos { get; set; }
        public List<OrdemCompra> OrdensCompra { get; set; }
    }
}
