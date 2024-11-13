using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using Microsoft.AspNetCore.Mvc;

namespace CidadeUnida.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAO repository;

        public UsuarioController(IConfiguration configuration)
        {
            repository = new UsuarioDAO(
                                    configuration.GetConnectionString(
                                       Configurations.AppSettings.GetKeyConnectionString()));
        }

        // GET: Usuario
        public IActionResult Index()
        {
            List<Usuario> usuarios = repository.GetAll();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public IActionResult Details(int id)
        {
            Usuario usuario = repository.GetByIdUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            try
            {
                repository.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Usuario usuario = repository.GetByIdUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            try
            {
                repository.Update(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
