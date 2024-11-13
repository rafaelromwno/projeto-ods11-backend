using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using Microsoft.AspNetCore.Mvc;

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
            List<Denuncia> denuncias = repository.GetAll();
            return View(denuncias);
        }

        // GET: Denuncia/Details/5
        public IActionResult Details(int id)
        {
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
            return View();
        }

        // POST: Denuncia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Denuncia denuncia)
        {
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
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
