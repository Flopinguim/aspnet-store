using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.DepartamentoViewModels;
using aspnet_store.Models.ViewModels.UsuarioViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public DepartamentoController(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public IActionResult Index()
        {
            var departamentos = dbContext.Departamentos.ToList();
            return View(departamentos);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDepartamentoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var departamento = new Departamento()
                {
                    Nome = viewModel.Nome
                };

                await dbContext.Departamentos.AddAsync(departamento);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Departamento");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var departamentos = await dbContext.Departamentos.ToListAsync();

            return View(departamentos);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var departamento = await dbContext.Departamentos.FindAsync(id);

            if (departamento is null)
                return NotFound();

            var viewModel = new AddDepartamentoViewModel
            {
                Id = departamento.Id,
                Nome = departamento.Nome,
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Departamento viewModel)
        {
            var departamento = await dbContext.Departamentos.FindAsync(viewModel.Id);

            if (departamento is not null)
            {
                departamento.Nome = viewModel.Nome;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Departamento");
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var departamento = await dbContext.Departamentos.FindAsync(id);
            if (departamento is null)
            {
                return NotFound();
            }

            var usuarioCount = await dbContext.Usuarios.CountAsync(u => u.DepartamentoId.Equals(id));

            var viewModel = new ConfirmDeleteViewModel
            {
                Departamento = departamento,
                UsuarioCount = usuarioCount
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departamento = await dbContext.Departamentos.FirstOrDefaultAsync(i => i.Id.Equals(id));
            if (departamento is not null)
            {
                dbContext.Departamentos.Remove(departamento);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Departamento");
        }

    }
}
