using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.OrdemCompraViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace aspnet_store.Controllers
{
    public class OrdemCompraController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public OrdemCompraController(ApplicationDbContext context) => dbContext = context;

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var ordensCompra = await dbContext.OrdensCompra
                .Include(o => o.Fornecedor)
                .Include(o => o.Pedidos)
                .ThenInclude(p => (p as PedidoProduto).Produto)
                .Include(o => o.Pedidos)
                .ThenInclude(p => (p as PedidoServico).Servico)
                .ToListAsync();

            return View(ordensCompra);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var fornecedores = await dbContext.Fornecedores.ToListAsync();
            var pedidosProduto = await dbContext.Pedidos.OfType<PedidoProduto>().Include(p => p.Produto).Where(p => p.OrdemCompraId == null).ToListAsync();
            var pedidosServico = await dbContext.Pedidos.OfType<PedidoServico>().Include(p => p.Servico).Where(p => p.OrdemCompraId == null).ToListAsync();

            var viewModel = new OrdemCompraViewModel
            {
                Fornecedores = fornecedores,
                PedidosProduto = pedidosProduto,
                PedidosServico = pedidosServico,
                DataCadastro = DateTime.UtcNow
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(OrdemCompraViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Fornecedores));
            ModelState.Remove(nameof(viewModel.FornecedorNome));
            if (ModelState.IsValid)
            {
                var ordemCompra = new OrdemCompra
                {
                    FornecedorId = viewModel.FornecedorId,
                    DataCadastro = viewModel.DataCadastro,
                    Pedidos = new List<Pedido>()
                };

                var selectedPedidoProdutoIds = viewModel.SelectedPedidoProdutoIds ?? new List<int>();
                var selectedPedidoServicoIds = viewModel.SelectedPedidoServicoIds ?? new List<int>();

                if (!selectedPedidoProdutoIds.Any() && !selectedPedidoServicoIds.Any())
                {
                    ModelState.AddModelError(string.Empty, "Selecione ao menos um pedido de produto ou serviço.");
                    viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync();
                    viewModel.PedidosProduto = await dbContext.Pedidos.OfType<PedidoProduto>().Include(p => p.Produto).Where(p => p.OrdemCompraId == null).ToListAsync();
                    viewModel.PedidosServico = await dbContext.Pedidos.OfType<PedidoServico>().Include(p => p.Servico).Where(p => p.OrdemCompraId == null).ToListAsync();
                    return View(viewModel);
                }

                var pedidosProduto = dbContext.Pedidos.OfType<PedidoProduto>().Where(p => selectedPedidoProdutoIds.Contains(p.Id)).ToList();
                var pedidosServico = dbContext.Pedidos.OfType<PedidoServico>().Where(p => selectedPedidoServicoIds.Contains(p.Id)).ToList();

                pedidosProduto.ForEach(p => p.OrdemCompra = ordemCompra);
                pedidosServico.ForEach(p => p.OrdemCompra = ordemCompra);

                ordemCompra.Pedidos.AddRange(pedidosProduto);
                ordemCompra.Pedidos.AddRange(pedidosServico);

                dbContext.OrdensCompra.Add(ordemCompra);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }

            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync();
            viewModel.PedidosProduto = await dbContext.Pedidos.OfType<PedidoProduto>().Include(p => p.Produto).Where(p => p.OrdemCompraId == null).ToListAsync();
            viewModel.PedidosServico = await dbContext.Pedidos.OfType<PedidoServico>().Include(p => p.Servico).Where(p => p.OrdemCompraId == null).ToListAsync();
            return View(viewModel);
        }
    }
}
