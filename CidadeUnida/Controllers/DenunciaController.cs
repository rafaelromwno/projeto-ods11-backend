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

        // Exibe uma lista de todas as denúncias
        [HttpGet]
        public async Task<IActionResult> ListarDenuncias()
        {
            List<Denuncia> denuncias = await repository.ListarTodasDenuncias();
            return View(denuncias); // Retorna a view "ListarDenuncias" com a lista de denúncias
        }

        // Exibe detalhes de uma denúncia específica
        [HttpGet("{id}")]
        [Route("Denuncia/Detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            Denuncia denuncia = await repository.ObterDenunciaPorId(id);
            if (denuncia == null)
            {
                return NotFound();
            }
            return View(denuncia); // Retorna a view "Detalhes" com a denúncia especificada
        }

        // Exibe o formulário para criar uma nova denúncia
        [HttpGet]
        public IActionResult Criar()
        {
            return View(); // Retorna a view "Criar" com o formulário de criação
        }

        // Processa o formulário de criação de uma nova denúncia
        [HttpPost]
        public async Task<IActionResult> Criar(Denuncia denuncia)
        {
            if (ModelState.IsValid)
            {
                int novoId = await repository.InserirDenuncia(denuncia);
                return RedirectToAction(nameof(Detalhes), new { id = novoId });
            }
            return View(denuncia); // Retorna à view "Criar" em caso de erro
        }

        // Exibe o formulário para editar uma denúncia existente
        [HttpGet("{id}")]
        [Route("Denuncia/Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            Denuncia denuncia = await repository.ObterDenunciaPorId(id);
            if (denuncia == null)
            {
                return NotFound();
            }
            return View(denuncia); // Retorna a view "Editar" com a denúncia carregada
        }

        // Processa o formulário de edição de uma denúncia existente
        [HttpPost("{id}")]
        public async Task<IActionResult> Editar(int id, Denuncia denuncia)
        {
            if (id != denuncia.IdDenuncia || !ModelState.IsValid)
            {
                return BadRequest();
            }

            bool atualizado = await repository.AtualizarDenuncia(denuncia);
            if (!atualizado)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Detalhes), new { id = denuncia.IdDenuncia });
        }

        // Exibe a confirmação para exclusão de uma denúncia
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Deletar(int id)
        //{
        //    Denuncia denuncia = await repository.ObterDenunciaPorId(id);
        //    if (denuncia == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(denuncia); // Retorna a view "Deletar" para confirmação
        //}

        // Processa a exclusão de uma denúncia
        [HttpPost("{id}"), ActionName("Deletar")]
        [Route("Denuncia/Deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            bool excluido = await repository.DeletarDenuncia(id);
            if (!excluido)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(ListarDenuncias));
        }
    }
}
