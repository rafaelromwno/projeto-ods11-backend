using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using CidadeUnida.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CidadeUnida.Controllers
{
    public class ContatoController : Controller
    {
        private readonly ContatoDAO repository;

        public ContatoController(IConfiguration configuration)
        {
            repository = new ContatoDAO(
                                    configuration.GetConnectionString(
                                       Configurations.AppSettings.GetKeyConnectionString()));
        }

        // GET: Contato
        public IActionResult Index()
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            List<Contato> contatos = repository.GetAll();
            return View(contatos);

        }

        // GET: Contato/Details/5
        public IActionResult Details(int id)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            Contato contato = repository.GetByIdContato(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);

        }

        // GET: Contato/Create
        public IActionResult Create()
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            return View();
        }

        // POST: Contato/Create
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repository.Add(contato);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contato/Edit/5
        public IActionResult Edit(int id)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            Contato contato = repository.GetByIdContato(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato);
        }

        // POST: Contato/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contato contato)
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repository.Update(id, contato);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contato/Delete/5
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
