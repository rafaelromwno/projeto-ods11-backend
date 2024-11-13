using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using Microsoft.AspNetCore.Mvc;

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
            List<Contato> contatos = repository.GetAll();
            return View(contatos);
        }

        // GET: Contato/Details/5
        public IActionResult Details(int id)
        {
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
            return View();
        }

        // POST: Contato/Create
        [HttpPost]
        public IActionResult Create(Contato contato)
        {
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
            repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
