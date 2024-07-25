using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.PedidoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PedidoController(ApplicationDbContext context)
        {
            dbContext = context;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddPedidoViewModel
            {
                Departamentos = await dbContext.Departamentos.ToListAsync(),
                Usuarios = await dbContext.Usuarios.ToListAsync(),
                Produtos = await dbContext.Produtos.ToListAsync(),
                Servicos = await dbContext.Servicos.ToListAsync(),
                Fornecedores = await dbContext.Fornecedores.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPedidoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Departamentos));
            ModelState.Remove(nameof(viewModel.Usuarios));
            ModelState.Remove(nameof(viewModel.Produtos));
            ModelState.Remove(nameof(viewModel.Servicos));
            ModelState.Remove(nameof(viewModel.Fornecedores));

            if (viewModel.TipoPedido == "Produto")
            {
                ModelState.Remove(nameof(viewModel.ServicoId));
                ModelState.Remove(nameof(viewModel.FornecedorId));
                ModelState.Remove(nameof(viewModel.Observacao));

                if (viewModel.ProdutoId == null)
                {
                    ModelState.AddModelError(nameof(viewModel.ProdutoId), "Produto é obrigatório.");
                }

                if (viewModel.Quantidade == null)
                {
                    ModelState.AddModelError(nameof(viewModel.Quantidade), "Quantidade é obrigatória.");
                }
            }
            else if (viewModel.TipoPedido == "Servico")
            {
                ModelState.Remove(nameof(viewModel.ProdutoId));
                ModelState.Remove(nameof(viewModel.Quantidade));

                if (viewModel.ServicoId == null)
                {
                    ModelState.AddModelError(nameof(viewModel.ServicoId), "Serviço é obrigatório.");
                }

                if (viewModel.FornecedorId == null)
                {
                    ModelState.AddModelError(nameof(viewModel.FornecedorId), "Fornecedor é obrigatório.");
                }
            }
            else
            {
                ModelState.AddModelError(nameof(viewModel.TipoPedido), "Tipo de Pedido é obrigatório.");
            }

            if (ModelState.IsValid)
            {
                if (viewModel.TipoPedido == "Produto")
                {
                    var pedidoProduto = new PedidoProduto
                    {
                        DepartamentoId = viewModel.DepartamentoId,
                        UsuarioSolicitanteId = viewModel.UsuarioSolicitanteId,
                        DataCadastro = viewModel.DataCadastro,
                        ProdutoId = viewModel.ProdutoId.Value,
                        Quantidade = viewModel.Quantidade.Value
                    };

                    dbContext.PedidoProdutos.Add(pedidoProduto);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                else if (viewModel.TipoPedido == "Servico")
                {
                    var pedidoServico = new PedidoServico
                    {
                        DepartamentoId = viewModel.DepartamentoId,
                        UsuarioSolicitanteId = viewModel.UsuarioSolicitanteId,
                        DataCadastro = viewModel.DataCadastro,
                        ServicoId = viewModel.ServicoId.Value,
                        FornecedorId = viewModel.FornecedorId.Value,
                        Observacao = viewModel.Observacao
                    };

                    dbContext.PedidoServicos.Add(pedidoServico);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
            }

            viewModel.Departamentos = await dbContext.Departamentos.ToListAsync();
            viewModel.Usuarios = await dbContext.Usuarios.ToListAsync();
            viewModel.Produtos = await dbContext.Produtos.ToListAsync();
            viewModel.Servicos = await dbContext.Servicos.ToListAsync();
            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var pedidosProduto = await dbContext.PedidoProdutos
                .Include(p => p.Produto)
                .Include(p => p.Departamento)
                .Include(p => p.UsuarioSolicitante)
                .ToListAsync();

            var pedidosServico = await dbContext.PedidoServicos
                .Include(p => p.Servico)
                .Include(p => p.Fornecedor)
                .Include(p => p.Departamento)
                .Include(p => p.UsuarioSolicitante)
                .ToListAsync();

            var viewModel = new ListPedidoViewModel
            {
                PedidosProduto = pedidosProduto,
                PedidosServico = pedidosServico
            };

            return View(viewModel);
        }
    }
}
