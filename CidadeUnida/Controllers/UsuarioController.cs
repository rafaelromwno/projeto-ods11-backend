using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using CidadeUnida.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            List<Usuario> usuarios = repository.GetAll();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public IActionResult Details(int id)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

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
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

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
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

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
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

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
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private IActionResult VerificarPermissao()
        {
            var loginTokenJson = HttpContext.Session.GetString("login");
            if (loginTokenJson == null)
            {
                TempData["ErrorMessage"] = "É necessário estar logado para realizar essa operação!";

                return RedirectToAction("Login", "Login");
            }

            var login = JsonConvert.DeserializeObject<LoginViewModel>(loginTokenJson);
            if (!login.IsAdm)
            {
                return RedirectToAction("AcessoNegado", "Home");
            }

            return null; // Indica que o acesso está permitido
        }
    }
}
