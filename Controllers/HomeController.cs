using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaVendas.Models;
using SistemaVendas.Uteis;

namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //criar parametro opcional que será usado no logout
        public IActionResult Login(int? id)
        {
            //para realizar logout, limpa as variaveis de sessão
            if(id != null)
            {
                if(id == 0)
                {
                    HttpContext.Session.SetString("idUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("nomeUsuarioLogado", string.Empty);
                }
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                bool loginOk = login.validarLogin();
                if (loginOk)
                {
                    //idUsuarioLogado é o nome da variavel de sessão
                    //guarda as informações de login dentro da sessão
                    HttpContext.Session.SetString("idUsuarioLogado",login.id);
                    HttpContext.Session.SetString("nomeUsuarioLogado", login.nome);
                    //se o login estiver ok, direciona o usuario para a pagina Menu que tem a controladora Home
                    return RedirectToAction("Menu","Home");
                }
                else
                {
                    //variavel temporaria
                    TempData["ErrorLogin"] = "Email ou Senha invalidos!";
                }
            }

            return View();
        }
        //é necessário criar uma nova IActionResult para cada view
        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
