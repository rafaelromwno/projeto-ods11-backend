using CidadeUnida.Repositories.ADO.SQL_Server;
using CidadeUnida.Services;
using Microsoft.AspNetCore.Mvc;
using CidadeUnida.Configurations;
using NuGet.Protocol.Plugins;
using CidadeUnida.Models;
using CidadeUnida.ViewModels;

namespace CidadeUnida.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginDAO repository;
        private readonly EditarPerfilDAO repositoryPerfil;
        private readonly ISessao sessao;

        public LoginController(IConfiguration configuration, ISessao sessao)
        {
            repository = new LoginDAO(configuration.GetConnectionString(AppSettings.GetKeyConnectionString()));
            this.sessao = sessao;
            repositoryPerfil = new EditarPerfilDAO(
                                    configuration.GetConnectionString(AppSettings.GetKeyConnectionString()));
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Se o usuário não estiver logado retorna a View() senão retorna para a página de início.
            return sessao.GetTokenLogin() == null ? View() : RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                // Verificar as credenciais
                if (repository.Check(loginViewModel))
                {
                    var loginResultado = repository.GetTipo(loginViewModel);

                    // Define as propriedades adicionais do loginViewModel
                    loginViewModel.IsAdm = loginResultado.IsAdm;
                    loginViewModel.IdUsuario = loginResultado.Id; // Obtenha o ID do usuário

                    // Armazena informações na sessão
                    sessao.AddTokenLogin(loginViewModel);

                    TempData["SuccessMessage"] = "Login Realizado!";

                    // Redireciona com base no tipo de usuário 
                    if (loginResultado.IsAdm)
                    {
                        return RedirectToAction("Painel", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                // Credenciais inválidas
                ModelState.AddModelError(string.Empty, "Usuário e/ou Senha Inválidos!");
            }

            // Retorna a view com mensagens de erro, se houver
            return View(loginViewModel);
        }

        // GET: Usuario/Edit/5
        [HttpGet]
        public IActionResult EditarPerfil(int id)
        {
            var perfil = repositoryPerfil.ObterPerfil(id);
            if (perfil == null)
            {
                return NotFound();
            }
            return View(perfil);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarPerfil(PerfilViewModel perfil)
        {
            if (ModelState.IsValid)
            {
                bool sucesso = repositoryPerfil.AtualizarPerfil(perfil);
                if (sucesso)
                {
                    TempData["SuccessMessage"] = "Perfil atualizado!";

                    return View(perfil);
                }
                else
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao atualizar o perfil.");
                }
            }
            return View(perfil);
        }

        public IActionResult Logout()
        {
            sessao.DeleteTokenLogin();
            return RedirectToAction("Index", "Home");
        }
    }
}
