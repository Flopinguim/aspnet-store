using aspnet_store.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.EstoqueViewModels
{
    public class EntradaProdutoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ordem de Compra é obrigatória")]
        public int OrdemCompraId { get; set; }
        public OrdemCompra OrdemCompra { get; set; }

        [Required(ErrorMessage = "Nota Fiscal é obrigatória")]
        public string NotaFiscal { get; set; }

        [Required(ErrorMessage = "Quantidade é obrigatória")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Depósito é obrigatório")]
        public string Deposito { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        public List<OrdemCompra> OrdensCompra { get; set; }
    }
}
