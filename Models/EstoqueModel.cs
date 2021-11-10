using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SistemaVendas.Uteis;


namespace SistemaVendas.Models
{
    public class EstoqueModel
    {
        public decimal? preco_unitario { get; set; }
        public decimal? quantidade_estoque { get; set; }
        public string id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string link_foto { get; set; }
        public double total { get; set; }
        public string listaProdutos { get; set; }

        public List<EstoqueModel> ListarProdutosEstoque()
        {
            List<EstoqueModel> lista = new List<EstoqueModel>();
            EstoqueModel produto = new EstoqueModel();
            DAL objDAL = new DAL();
            string sql = "select e1.id_produto, v2.nome as nome, e1.qtde, e1.valor_unitario, v2.link_foto from estoque e1 inner join produto v2 on e1.id_produto = v2.id order by nome";

            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                produto = new EstoqueModel()
                {
                    id = dt.Rows[i]["id_produto"].ToString(),
                    nome = dt.Rows[i]["nome"].ToString(),
                    quantidade_estoque = decimal.Parse(dt.Rows[i]["qtde"].ToString()),
                    preco_unitario = decimal.Parse(dt.Rows[i]["valor_unitario"].ToString()),
                    link_foto = dt.Rows[i]["link_foto"].ToString()
                };
                lista.Add(produto);
            }
            return lista;
        }

        public void gravar()
        {
            DAL objDAL = new DAL();
            string dataEntrada = DateTime.Now.Date.ToString("yyyy/MM/dd");
            List<ItemEstoqueModel> ListaItens = JsonConvert.DeserializeObject<List<ItemEstoqueModel>>(listaProdutos);
            for (int i = 0; i < ListaItens.Count; i++)
            {
                string sql = $"insert into entrada_produto (id_produto, qtde, valor_unitario, data_entrada)" +
                 $"values('{ListaItens[i].codigoProduto}', '{ListaItens[i].qtdeProduto.ToString()}', '{preco_unitario.ToString().Replace(",", ".")}', '{dataEntrada}')";
                objDAL.executarComandoSql(sql);
            }

        }
    }
}
