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
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await repository.ObterTodosUsuarios();
            return View(usuarios);
        }

        // GET: Usuario/Detalhes/5
        public async Task<IActionResult> Detalhes(int id)
        {
            var usuario = await repository.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Criar
        public IActionResult Criar()
        {
            return View();
        }

        // POST: Usuario/Criar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                int novoId = await repository.InserirUsuario(usuario);
                return RedirectToAction(nameof(ListarUsuarios));
            }

            return View(usuario);
        }

        // GET: Usuario/Editar/5
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await repository.ObterUsuarioPorId(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest("ID do usuário não corresponde ao ID na URL.");
            }

            if (ModelState.IsValid)
            {
                bool atualizado = await repository.AtualizarUsuario(usuario);
                if (atualizado)
                {
                    return RedirectToAction(nameof(ListarUsuarios));
                }

                ModelState.AddModelError("", "Erro ao atualizar o usuário.");
            }

            return View(usuario);
        }

        //// GET: Usuario/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var usuario = await repository.ObterUsuarioPorId(id);

        //    if (usuario == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(usuario);
        //}

        // POST: Usuario/Delete/5
        [HttpPost("{id}")]
        [Route("Usuario/Deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            bool excluido = await repository.ExcluirUsuario(id);
            if (excluido)
            {
                return RedirectToAction(nameof(ListarUsuarios));
            }

            ModelState.AddModelError("", "Erro ao excluir o usuário.");
            return View();
        }
    }
}
