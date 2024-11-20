using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using CidadeUnida.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CidadeUnida.Controllers
{
    public class DenunciaController : Controller
    {
        private readonly DenunciaDAO repository;

        public DenunciaController(IConfiguration configuration)
        {
            repository = new DenunciaDAO(
                                     configuration.GetConnectionString(
                                        Configurations.AppSettings.GetKeyConnectionString()));
        }

        // GET: Denuncia
        public IActionResult Index()
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            List<Denuncia> denuncias = repository.GetAll();
            return View(denuncias);
        }

        // GET: Denuncia/Details/5
        public IActionResult Details(int id)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            Denuncia denuncia = repository.GetByIdDenuncia(id);
            if (denuncia == null)
            {
                return NotFound();
            }
            return View(denuncia);
        }

        // GET: Denuncia/Create
        public IActionResult Create()
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            return View();
        }

        // POST: Denuncia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Denuncia denuncia)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repository.Add(denuncia);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Denuncia/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            Denuncia denuncia = repository.GetByIdDenuncia(id);
            if (denuncia == null)
            {
                return NotFound();
            }
            return View(denuncia);
        }

        // POST: Denuncia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Denuncia denuncia)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repository.Update(id, denuncia);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Denuncia/Delete/5
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
