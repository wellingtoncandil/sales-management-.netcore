using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class EstoqueController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaProdutos = new EstoqueModel().ListarProdutosEstoque();
            return View();
        }

        [HttpGet]
        public IActionResult Entrada()
        {
            ViewBag.ListaProdutos = new VendaModel().retornarListaProdutos();
            return View();
        }
        [HttpPost]
        public IActionResult Entrada(EstoqueModel estoque)
        {
            //verificar se o modelo está valido, ou seja, se os campos passaram pela 
            //validação de formulario
            if (ModelState.IsValid)
            {
                estoque.gravar();
                //retorna para a pagina index, mostrando o vendedor adicionado
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult DetalheProduto(int? id)
        {
            if (id != null)
            {
                ViewBag.Produto = new ProdutoModel().retornarProduto(id);
            }
            return View();
        }
    }
}
