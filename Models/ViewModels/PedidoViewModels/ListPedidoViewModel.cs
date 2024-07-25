using aspnet_store.Models.Entities;

namespace aspnet_store.Models.ViewModels.PedidoViewModels
{
    public class ListPedidoViewModel
    {
        public List<PedidoProduto> PedidosProduto { get; set; }
        public List<PedidoServico> PedidosServico { get; set; }
    }
}
