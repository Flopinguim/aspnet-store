using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.ProdutoViewModels;
using aspnet_store.Models.ViewModels.ServicoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class ServicoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ServicoController(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = new AddServicoViewModel
            {
                DataInicial = DateTime.UtcNow,
                DataFinal = DateTime.UtcNow.AddDays(1),
                PrazoEntrega = 1,
                Fornecedores = await dbContext.Fornecedores.ToListAsync(),
            };
            return View(viewModel); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddServicoViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.DataInicial));
            ModelState.Remove(nameof(viewModel.DataFinal));
            ModelState.Remove(nameof(viewModel.Fornecedores));
            ModelState.Remove(nameof(viewModel.FornecedorNome));

            if (ModelState.IsValid)
            {
                var servico = new Servico()
                {
                    Nome = viewModel.Nome,
                    FornecedorId = viewModel.FornecedorId,
                    Descricao = viewModel.Descricao,
                    Preco = viewModel.Preco,
                    PrazoEntrega = viewModel.PrazoEntrega,
                };

                await dbContext.Servicos.AddAsync(servico);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Servico");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var servicos = await dbContext.Servicos
                .Include(s => s.Fornecedor)
                .ToListAsync();

            return View(servicos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var servico = await dbContext.Servicos.FindAsync(id);
            if (servico == null)
            {
                return NotFound();
            }

            var viewModel = new AddServicoViewModel
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                Preco = servico.Preco,
                PrazoEntrega = servico.PrazoEntrega,
                FornecedorId = servico.FornecedorId,
                FornecedorNome = servico.Fornecedor?.Nome
            };

            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync() ?? new List<Fornecedor>();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddServicoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var servico = await dbContext.Servicos.FindAsync(viewModel.Id);
                if (servico == null)
                {
                    return NotFound();
                }

                servico.Nome = viewModel.Nome;
                servico.Descricao = viewModel.Descricao;
                servico.PrazoEntrega = viewModel.PrazoEntrega;
                servico.Preco = viewModel.Preco;
                servico.FornecedorId = viewModel.FornecedorId;

                dbContext.Servicos.Update(servico);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List");
            }

            viewModel.Fornecedores = await dbContext.Fornecedores.ToListAsync() ?? new List<Fornecedor>();
            return View(viewModel);
        }
    }
}
