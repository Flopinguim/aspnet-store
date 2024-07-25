using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.EstoqueViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EstoqueController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListEntrada()
        {
            var entradas = await dbContext.EntradasProduto.Include(e => e.OrdemCompra).ToListAsync();
            return View(entradas);
        }

        [HttpGet]
        public async Task<IActionResult> EntradaProduto()
        {
            var viewModel = new EntradaProdutoViewModel
            {
                OrdensCompra = await dbContext.OrdensCompra.Include(o => o.Fornecedor).ToListAsync(),
                DataCadastro = DateTime.UtcNow
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ListSaida()
        {
            var saidas = await dbContext.SaidasProduto.Include(s => s.OrdemCompra).ToListAsync();
            return View(saidas);
        }

        [HttpGet]
        public async Task<IActionResult> SaidaProduto()
        {
            var viewModel = new SaidaProdutoViewModel
            {
                OrdensCompra = await dbContext.OrdensCompra.Include(o => o.Fornecedor).ToListAsync(),
                Usuarios = await dbContext.Usuarios.ToListAsync(),
                Departamentos = await dbContext.Departamentos.ToListAsync(),
                DataCadastro = DateTime.UtcNow
            };

            return View(viewModel);
        }

        public async Task<IActionResult> SaidaProduto(SaidaProdutoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.OrdensCompra));
            ModelState.Remove(nameof(viewModel.Usuarios));
            ModelState.Remove(nameof(viewModel.Departamentos));

            if (ModelState.IsValid)
            {
                var saidaProduto = new SaidaProduto
                {
                    OrdemCompraId = viewModel.OrdemCompraId,
                    Quantidade = viewModel.Quantidade,
                    Usuario = viewModel.Usuario,
                    DepartamentoSolicitante = viewModel.DepartamentoSolicitante,
                    DataCadastro = viewModel.DataCadastro
                };

                dbContext.SaidasProduto.Add(saidaProduto);

                var produtoId = dbContext.OrdensCompra
                    .Include(o => o.Pedidos)
                    .ThenInclude(p => (p as PedidoProduto).Produto)
                    .FirstOrDefault(o => o.Id == viewModel.OrdemCompraId)?
                    .Pedidos.OfType<PedidoProduto>()
                    .FirstOrDefault()?.ProdutoId;

                if (produtoId.HasValue)
                {
                    var produto = await dbContext.Produtos.FindAsync(produtoId.Value);
                    if (produto != null)
                    {
                        produto.Estoque -= saidaProduto.Quantidade;
                        dbContext.Update(produto);
                    }
                }

                await dbContext.SaveChangesAsync();
                return RedirectToAction("ListSaida");
            }

            viewModel.OrdensCompra = await dbContext.OrdensCompra.Include(o => o.Fornecedor).ToListAsync();
            viewModel.Usuarios = await dbContext.Usuarios.ToListAsync();
            viewModel.Departamentos = await dbContext.Departamentos.ToListAsync();

            return View(viewModel);
        }

        public async Task<IActionResult> EntradaProduto(EntradaProdutoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.OrdensCompra));
            ModelState.Remove(nameof(viewModel.OrdemCompra));
            if (ModelState.IsValid)
            {
                var entradaProduto = new EntradaProduto
                {
                    OrdemCompraId = viewModel.OrdemCompraId,
                    NotaFiscal = viewModel.NotaFiscal,
                    Quantidade = viewModel.Quantidade,
                    Deposito = viewModel.Deposito,
                    DataCadastro = viewModel.DataCadastro
                };

                dbContext.EntradasProduto.Add(entradaProduto);

                var produtoId = dbContext.OrdensCompra
                    .Include(o => o.Pedidos)
                    .ThenInclude(p => (p as PedidoProduto).Produto)
                    .FirstOrDefault(o => o.Id == viewModel.OrdemCompraId)?
                    .Pedidos.OfType<PedidoProduto>()
                    .FirstOrDefault()?.ProdutoId;

                if (produtoId.HasValue)
                {
                    var produto = await dbContext.Produtos.FindAsync(produtoId.Value);
                    if (produto != null)
                    {
                        produto.Estoque += entradaProduto.Quantidade;
                        dbContext.Update(produto);
                    }
                }

                await dbContext.SaveChangesAsync();
                return RedirectToAction("ListEntrada");
            }

            viewModel.OrdensCompra = await dbContext.OrdensCompra.Include(o => o.Fornecedor).ToListAsync();
            return View(viewModel);
        }


    }
}
