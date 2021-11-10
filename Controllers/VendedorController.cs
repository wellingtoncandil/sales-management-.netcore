using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult Index()
        {
            //retorno da lista de clientes
            ViewBag.ListaVendedores = new VendedorModel().listarTodosVendedores();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if(id != null)
            {
                ViewBag.Vendedor = new VendedorModel().retornarVendedor(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(VendedorModel vendedor)
        {
            //verificar se o modelo está valido, ou seja, se os campos passaram pela 
            //validação de formulario
            if (ModelState.IsValid)
            {
                vendedor.gravar();
                //retorna para a pagina index, mostrando o vendedor adicionado
                return RedirectToAction("Index");
            }
            return View();
        }

        //recebe o parametro id para fazer a exclusão
        public IActionResult Excluir(int id)
        {
            //cria uma view com o dado que será excluido(parametro)
            ViewData["idExcluir"] = id;
            return View();
        }

        public IActionResult ExcluirVendedor(int id)
        {
            new VendedorModel().excluir(id);
            return View();
        }


    }

}
