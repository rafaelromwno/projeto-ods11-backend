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

        // Exibe uma lista de contatos
        [HttpGet]
        public async Task<IActionResult> ListarContatos()
        {
            List<Contato> contatos = await repository.ObterTodosContatos();
            return View(contatos); // Retorna a view "ListarContatos" com a lista de contatos
        }

        // Exibe detalhes de um contato específico
        [HttpGet("{id}")]
        [Route("Contato/Detalhes/{id:int}")]
        public async Task<IActionResult> Detalhes(int id)
        {
            Contato contato = await repository.ObterContatoPorId(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato); // Retorna a view "Detalhes" com o contato especificado
        }

        // Exibe o formulário para criar um novo contato
        [HttpGet]
        public IActionResult Criar()
        {
            return View(); // Retorna a view "Criar" com o formulário de criação
        }

        // Processa o formulário de criação de um novo contato
        [HttpPost]
        public async Task<IActionResult> Criar(Contato contato)
        {
            if (ModelState.IsValid)
            {
                int novoId = await repository.InserirContato(contato);
                return RedirectToAction(nameof(Detalhes), new { id = novoId });
            }
            return View(contato); // Retorna à view "Criar" em caso de erro
        }

        // Exibe o formulário para editar um contato existente
        [HttpGet("{id}")]
        [Route("Contato/Editar/{id:int}")]
        public async Task<IActionResult> Editar(int id)
        {
            Contato contato = await repository.ObterContatoPorId(id);
            if (contato == null)
            {
                return NotFound();
            }
            return View(contato); // Retorna a view "Editar" com o contato carregado
        }

        // Processa o formulário de edição de um contato existente
        [HttpPost("{id}")]
        public async Task<IActionResult> Editar(int id, Contato contato)
        {
            if (id != contato.IdContato || !ModelState.IsValid)
            {
                return BadRequest();
            }

            bool atualizado = await repository.AtualizarContato(contato);
            if (!atualizado)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Detalhes), new { id = contato.IdContato });
        }

        // Exibe a confirmação para exclusão de um contato
        //[HttpGet("{id}")]
        //[Route("Contato/Deletar/{id:int}")]
        //public async Task<IActionResult> Deletar(int id)
        //{
        //    Contato contato = await repository.ObterContatoPorId(id);
        //    if (contato == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contato); // Retorna a view "Deletar" para confirmação
        //}

        // Processa a exclusão de um contato
        [HttpPost("{id}")]
        [Route("Contato/Deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            bool excluido = await repository.ExcluirContato(id);
            if (!excluido)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(ListarContatos));
        }
    }
}
