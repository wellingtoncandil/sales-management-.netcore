using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        private HttpContextAccessor httpContext;

        //recebe o contexto http via injeção de dependencias
        public VendaController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = (HttpContextAccessor)HttpContextAccessor;

        }

        public IActionResult Index()
        {
            ViewBag.ListaVendas = new VendaModel().listaVendas();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            carregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            //captura o id do vendedor logado no sistema
            venda.vendedor_id = httpContext.HttpContext.Session.GetString("idUsuarioLogado");
            venda.inserir();
            carregarDados();
            return View();
        }

        //método que preenche as viewbags
        private void carregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().retornarListaClientes();
            ViewBag.ListaVendedores = new VendaModel().retornarListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().retornarListaProdutos();

        }
        [HttpGet]
        public IActionResult VisualizarVenda(int? id)
        {
            if (id != null)
            {
                ViewBag.Venda = new VendaModel().retornarVenda(id);
                ViewBag.ItensVenda = new VendaModel().retornarItensVenda(id);
            }
            return View();

        }
    }
}
