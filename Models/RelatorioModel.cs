using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class RelatorioModel
    {

        public DateTime DataDe { get; set; }

        public DateTime DataAte { get; set; }

    }

    public class GraficoProdutos
    {
        public double qtdeVendido { get; set; }
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; }

        public List<GraficoProdutos> retornarDadosGrafico()
        {
            DAL objDAL = new DAL();
            string sql = "select sum(qtde_produto) as qtde, p.nome as produto from itens_venda i inner join produto p on i.produto_id = p.id group by p.nome";
            DataTable dt = objDAL.RetDataTable(sql);

            List<GraficoProdutos> lista = new List<GraficoProdutos>();
            GraficoProdutos item;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GraficoProdutos();
                item.qtdeVendido = Double.Parse(dt.Rows[i]["qtde"].ToString());
                item.descricaoProduto = dt.Rows[i]["produto"].ToString();
                lista.Add(item);
            }
            return lista;
        }

    }
}
