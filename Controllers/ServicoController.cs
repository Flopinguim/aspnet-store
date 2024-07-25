using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.ServicoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace aspnet_store.Controllers
{
    public class ServicoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ServicoController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> List()
        {
            var servicos = await dbContext.Servicos.Include(s => s.Fornecedor).ToListAsync();
            return View(servicos);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var fornecedores = await dbContext.Fornecedores.ToListAsync();
            var viewModel = new AddServicoViewModel
            {
                Fornecedores = fornecedores,
                DataInicial = DateTime.UtcNow,
                DataFinal = DateTime.UtcNow.AddDays(7),
                PrazoEntrega = 7
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddServicoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Fornecedores));
            if (ModelState.IsValid)
            {
                var servico = new Servico
                {
                    Nome = viewModel.Nome,
                    FornecedorId = viewModel.FornecedorId,
                    Descricao = viewModel.Descricao,
                    Preco = viewModel.Preco,
                    DataInicial = viewModel.DataInicial,
                    DataFinal = viewModel.DataFinal,
                    PrazoEntrega = viewModel.PrazoEntrega
                };

                dbContext.Servicos.Add(servico);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }

            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync();
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var servico = await dbContext.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound();

            var fornecedores = await dbContext.Fornecedores.ToListAsync();
            var viewModel = new AddServicoViewModel
            {
                Id = servico.Id,
                Nome = servico.Nome,
                FornecedorId = servico.FornecedorId,
                FornecedorNome = servico.Fornecedor.Nome,
                Fornecedores = fornecedores,
                Descricao = servico.Descricao,
                Preco = servico.Preco,
                DataInicial = servico.DataInicial,
                DataFinal = servico.DataFinal,
                PrazoEntrega = servico.PrazoEntrega
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddServicoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Fornecedores));
            if (ModelState.IsValid)
            {
                var servico = await dbContext.Servicos.FindAsync(viewModel.Id);
                if (servico != null)
                {
                    servico.Nome = viewModel.Nome;
                    servico.FornecedorId = viewModel.FornecedorId;
                    servico.Descricao = viewModel.Descricao;
                    servico.Preco = viewModel.Preco;
                    servico.DataInicial = viewModel.DataInicial;
                    servico.DataFinal = viewModel.DataFinal;
                    servico.PrazoEntrega = viewModel.PrazoEntrega;

                    await dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                return NotFound();
            }

            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await dbContext.Servicos.FindAsync(id);
            if (servico == null)
                return NotFound();

            dbContext.Servicos.Remove(servico);
            await dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
