using aspnet_store.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aspnet_store.Models.ViewModels.OrdemCompraViewModels
{
    public class OrdemCompraViewModel
    {
        public OrdemCompraViewModel()
        {
            PedidosProduto = new List<PedidoProduto>();
            PedidosServico = new List<PedidoServico>();
            SelectedPedidoProdutoIds = new List<int>();
            SelectedPedidoServicoIds = new List<int>();
        }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        public DateTime DataCadastro { get; set; }

        public List<Fornecedor> Fornecedores { get; set; }
        public List<PedidoProduto> PedidosProduto { get; set; }
        public List<PedidoServico> PedidosServico { get; set; }

        [Display(Name = "Pedidos de Produto")]
        public List<int> SelectedPedidoProdutoIds { get; set; }

        [Display(Name = "Pedidos de Serviço")]
        public List<int> SelectedPedidoServicoIds { get; set; }
    }
}
