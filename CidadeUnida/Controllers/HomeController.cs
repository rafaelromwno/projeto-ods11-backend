﻿using CidadeUnida.Models;
using CidadeUnida.Repositories.ADO.SQL_Server;
using CidadeUnida.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;

namespace CidadeUnida.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly DenunciaDAO repository;
        private readonly ContatoDAO repositoryContato;
        private readonly UsuarioDAO repositoryUsuario;

        public HomeController(IConfiguration configuration, IConfiguration configurationContato)
        {
            repository = new DenunciaDAO(
                                     configuration.GetConnectionString(
                                        Configurations.AppSettings.GetKeyConnectionString()));
            repositoryContato = new ContatoDAO(
                                    configuration.GetConnectionString(
                                       Configurations.AppSettings.GetKeyConnectionString()));
            repositoryUsuario = new UsuarioDAO(
                                    configuration.GetConnectionString(
                                       Configurations.AppSettings.GetKeyConnectionString()));
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/CreateDenuncia
        public IActionResult CreateDenuncia()
        {
            return View();
        }

        // POST: Home/CreateDenuncia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDenuncia(Denuncia denuncia)
        {
            var isLogado = VerificarSeEstaLogado();
            if (isLogado != null)
            {
                return isLogado; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repository.Add(denuncia);

                TempData["SuccessMessage"] = "Sua denúncia foi enviada! Pequenas atitudes, grandes mudanças!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/CreateContato
        public IActionResult Ajuda()
        {
            return View();
        }

        // POST: Contato/CreateContato
        [HttpPost]
        public IActionResult Ajuda(Contato contato)
        {
            var isLogado = VerificarSeEstaLogado();
            if (isLogado != null)
            {
                return isLogado; // Redireciona se o usuário não tiver permissão
            }

            try
            {
                repositoryContato.Add(contato);

                TempData["SuccessMessage"] = "Sua mensagem foi enviada! Obrigado por nos contatar!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/CreateUsuario
        public IActionResult CreateUsuario()
        {
            return View();
        }

        // POST: Home/CreateUsuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUsuario(Usuario usuario)
        {
            /*try
            {

                repositoryUsuario.Add(usuario);

                TempData["SuccessMessage"] = "Seu Cadastro foi realizado!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(); // Redirecionará para o usuario futuramente!
            }*/

            if (ModelState.IsValid)
            {
                repositoryUsuario.Add(usuario);

                TempData["SuccessMessage"] = "Seu Cadastro foi realizado!";

                return RedirectToAction("Login", "Login");
            }

            // Linha para inspecionar erros de validação
            /*var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            foreach (var error in errors)
            {
                Console.WriteLine(error); // ou use um log para registrar os erros
            }*/

            return View(usuario);
            
            
        }

        public IActionResult Painel()
        {
            var permissao = VerificarPermissao();
            if (permissao != null)
            {
                return permissao; // Redireciona se o usuário não tiver permissão
            }

            return View();
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        public IActionResult EsqueciSenha()
        {
            return View();
        }              

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

        private IActionResult VerificarSeEstaLogado()
        {
            var loginTokenJson = HttpContext.Session.GetString("login");
            if (loginTokenJson == null)
            {
                TempData["ErrorMessage"] = "É necessário estar logado para realizar essa operação!";

                return RedirectToAction("Login", "Login");
            }           

            return null; // Indica que o acesso está permitido
        }
    }
}
