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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var servico = await dbContext.Servicos.FindAsync(id);

            if (servico is null)
                return NotFound();

            var viewModel = new AddServicoViewModel
            {
                Id = servico.Id,
                Nome = servico.Nome,
                FornecedorId = servico.FornecedorId,
                Descricao = servico.Descricao,
                PrazoEntrega = servico.PrazoEntrega,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Servico viewModel)
        {
            var servico = await dbContext.Servicos.FindAsync(viewModel.Id);

            if (servico is not null)
            {
                servico.Nome = viewModel.Nome;
                servico.FornecedorId = viewModel.Fornecedor.Id;
                servico.Descricao = viewModel.Descricao;
                servico.PrazoEntrega = viewModel.PrazoEntrega;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Servico");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var servico = await dbContext.Servicos
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (servico is not null)
            {
                dbContext.Servicos.Remove(servico);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Servico");
        }
    }
}
