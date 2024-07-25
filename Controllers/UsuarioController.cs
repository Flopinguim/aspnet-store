using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.UsuarioViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace aspnet_store.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsuarioController(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var departamentos = dbContext.Departamentos.ToList();
            var viewModel = new AddUsuarioViewModel
            {
                Departamentos = departamentos
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUsuarioViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Departamentos));
            ModelState.Remove(nameof(viewModel.DepartamentoNome));
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    Nome = viewModel.Nome,
                    Matricula = viewModel.Matricula,
                    DepartamentoId = viewModel.DepartamentoId
                };

                await dbContext.Usuarios.AddAsync(usuario);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("List", "Usuario");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var usuarios = await dbContext.Usuarios.Include(u => u.Departamento).ToListAsync();
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return NotFound();

            var usuario = await dbContext.Usuarios
                .Include(u => u.Departamento)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario is null)
                return NotFound();

            var viewModel = new AddUsuarioViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Matricula = usuario.Matricula,
                DepartamentoId = usuario.DepartamentoId,
                Departamentos = await dbContext.Departamentos.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddUsuarioViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Departamentos));
            ModelState.Remove(nameof(viewModel.DepartamentoNome));
            if (ModelState.IsValid)
            {
                var usuario = await dbContext.Usuarios.FindAsync(viewModel.Id);

                if (usuario is not null)
                {
                    usuario.Nome = viewModel.Nome;
                    usuario.Matricula = viewModel.Matricula;
                    usuario.DepartamentoId = viewModel.DepartamentoId;

                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("List", "Usuario");
                }
            }

            viewModel.Departamentos = await dbContext.Departamentos.ToListAsync();
            return View(viewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario is not null)
            {
                var pedidosAssociados = await dbContext.Pedidos
                    .AnyAsync(p => p.UsuarioSolicitanteId == id);

                if (pedidosAssociados)
                {
                    TempData["ErrorMessage"] = $"O usuário {usuario.Nome} tem pedidos associados. Não pode ser excluído.";
                    return RedirectToAction("List");
                }

                dbContext.Usuarios.Remove(usuario);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Usuario");
        }
    }
}
