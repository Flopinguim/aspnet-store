using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.DepartamentoViewModels;
using aspnet_store.Models.ViewModels.ProdutoViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProdutoController(ApplicationDbContext dbContext) => this.dbContext = dbContext;


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProdutoViewModel viewModel)
        {
            var produto = new Produto()
            {
                Nome = viewModel.Nome,
                CodigoEAN = viewModel.CodigoEAN,
                PrecoUnitario = viewModel.PrecoUnitario,
                CotaMinima = viewModel.CotaMinima,
            };

            await dbContext.Produtos.AddAsync(produto);

            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var produtos = await dbContext.Produtos.ToListAsync();

            return View(produtos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var produto = await dbContext.Produtos.FindAsync(id);

            if (produto is null)
                return NotFound();

            var viewModel = new AddProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                CodigoEAN = produto.CodigoEAN,
                PrecoUnitario = produto.PrecoUnitario,
                CotaMinima = produto.CotaMinima,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Produto viewModel)
        {
            var produto = await dbContext.Produtos.FindAsync(viewModel.Id);

            if (produto is not null)
            {
                produto.Nome = viewModel.Nome;
                produto.CodigoEAN = viewModel.CodigoEAN;
                produto.PrecoUnitario = viewModel.PrecoUnitario;
                produto.CotaMinima = viewModel.CotaMinima;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Produto");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await dbContext.Produtos
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (produto is not null)
            {
                dbContext.Produtos.Remove(produto);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Produto");
        }
    }
}
