using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.FornecedorViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_store.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public FornecedorController(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFornecedorViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.InscricaoEstadual));
            ModelState.Remove(nameof(viewModel.InscricaoMunicipal));

            if (ModelState.IsValid)
            {
                var fornecedor = new Fornecedor
                {
                    Nome = viewModel.Nome,
                    CNPJ = viewModel.CNPJ,
                    Email = viewModel.Email,
                    Endereco = viewModel.Endereco,
                    InscricaoEstadual = viewModel.InscricaoEstadual,
                    InscricaoMunicipal = viewModel.InscricaoMunicipal,
                };

                await dbContext.Fornecedores.AddAsync(fornecedor);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Fornecedor");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var fornecedors = await dbContext.Fornecedores.ToListAsync();
            return View(fornecedors);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var fornecedor = await dbContext.Fornecedores.FindAsync(id);

            if (fornecedor is null)
                return NotFound();

            var viewModel = new AddFornecedorViewModel
            {
                Id = fornecedor.Id,
                Nome = fornecedor.Nome,
                CNPJ = fornecedor.CNPJ,
                Email = fornecedor.Email,
                Endereco = fornecedor.Endereco,
                InscricaoEstadual = fornecedor.InscricaoEstadual ?? string.Empty,
                InscricaoMunicipal = fornecedor.InscricaoMunicipal ?? string.Empty,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Fornecedor viewModel)
        {
            var fornecedor = await dbContext.Fornecedores.FindAsync(viewModel.Id);

            if (fornecedor is not null)
            {
                fornecedor.Nome = viewModel.Nome;
                fornecedor.CNPJ = viewModel.CNPJ;
                fornecedor.Email = viewModel.Email;
                fornecedor.Endereco = viewModel.Endereco;
                fornecedor.InscricaoEstadual = viewModel.InscricaoEstadual;
                fornecedor.InscricaoMunicipal = viewModel.InscricaoMunicipal;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Fornecedor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var fornecedor = await dbContext.Fornecedores
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (fornecedor is not null)
            {
                var pedidosAssociados = await dbContext.PedidoServicos
                    .AnyAsync(p => p.FornecedorId == id);

                if (pedidosAssociados)
                {
                    TempData["ErrorMessage"] = $"O fornecedor {fornecedor.Nome} tem pedidos associados. Não pode ser excluído.";
                    return RedirectToAction("List");
                }

                dbContext.Fornecedores.Remove(fornecedor);
                await dbContext.SaveChangesAsync();
                TempData["SuccessMessage"] = $"O fornecedor {fornecedor.Nome} foi excluído com sucesso.";
            }

            return RedirectToAction("List", "Fornecedor");
        }
    }
}
