using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vendas()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Vendas(RelatorioModel relatorio)
        {
            if (relatorio.DataDe.Year == 1)
            {
                ViewBag.ListaVendas = new VendaModel().listaVendas();
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy/MM/dd");
                string DataAte = relatorio.DataAte.ToString("yyyy/MM/dd");
                ViewBag.ListaVendas = new VendaModel().listaVendas(DataDe, DataAte);
            }
            return View();
        }

        public IActionResult Grafico()
        {
            List<GraficoProdutos> lista = new GraficoProdutos().retornarDadosGrafico();
            string valores = "";
            string labels = "";
            string cores = "";

            var random = new Random();
            //percorre a lista de itens para compor o grafico 
            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].qtdeVendido.ToString() + ",";
                labels += "'" + lista[i].descricaoProduto.ToString() + "',";
                //escolher aleatoriamente as cores para compor as partes do grafico
                cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }

        public IActionResult Comissao()
        {
            return View();
        }
    }
}
