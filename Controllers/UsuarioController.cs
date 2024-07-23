using aspnet_store.Data;
using aspnet_store.Models.Entities;
using aspnet_store.Models.ViewModels.UsuarioViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnet_store.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsuarioController(ApplicationDbContext dbContext) => this.dbContext = dbContext;


        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new AddUsuarioViewModel
            {
                Departamentos = dbContext.Departamentos.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUsuarioViewModel viewModel)
        {
            ModelState.Remove(nameof(viewModel.Departamentos));
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

            viewModel.Departamentos = dbContext.Departamentos.ToList();
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

            var usuario = await dbContext.Usuarios.FindAsync(id);

            if (usuario is null)
                return NotFound();

            var viewModel = new AddUsuarioViewModel
            {
                Id = usuario.Id,
                Matricula = usuario.Matricula,
                Nome = usuario.Nome,
                DepartamentoId = usuario.DepartamentoId,
                Departamentos = dbContext.Departamentos.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario viewModel)
        {
            var usuario = await dbContext.Usuarios.FindAsync(viewModel.Id);

            if (usuario is not null)
            {
                usuario.Nome = viewModel.Nome;
                usuario.Matricula = viewModel.Matricula;
                usuario.DepartamentoId = viewModel.DepartamentoId;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Usuario");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await dbContext.Usuarios
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (usuario is not null)
            {
                dbContext.Usuarios.Remove(usuario);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Usuario");
        }
    }
}
