using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            //retorno da lista de clientes
            ViewBag.ListaClientes = new ClienteModel().listarTodosClientes();
            return View();
        }
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            if(id != null)
            {
                //carregar o registro do cliente em uma ViewBag
                ViewBag.Cliente = new ClienteModel().retornarCliente(id);
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

        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            //verificar se o modelo está valido, ou seja, se os campos passaram pela 
            //validação de formulario
            if (ModelState.IsValid)
            {
                cliente.gravar();
                //retorna para a pagina index, mostrando o cliente adicionado
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ExcluirCliente(int id)
        {
            new ClienteModel().excluir(id);
            return View();
        }

        public IActionResult Teste()
        {
            return View();
        }
    }
}
