using Newtonsoft.Json;
using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public string id { get; set; }

        public string data { get; set; }

        public string cliente_id { get; set; }

        public string nome_cliente { get; set; }

        public string vendedor_id { get; set; }

        public string listaProdutos { get; set; }

        public double total { get; set; }

        public string nomeVendedor { get; set; }

        // para atender o filtro do relatorio
        public List<VendaModel> listaVendas(string DataDe, string DataAte)
        {
            return retornarListaVendas(DataDe, DataAte);
        }

        // listagem geral
        public List<VendaModel> listaVendas()
        {
            return retornarListaVendas("1900/01/01", "2200/01/01");
        }
        public VendaModel retornarVenda(int? id)
        {
            VendaModel venda;
            DAL objDAL = new DAL();
            string sql = $"SELECT id, data, total, vendedor_id, cliente_id FROM Venda WHERE id='{id}' ORDER BY id asc";
            DataTable dt = objDAL.RetDataTable(sql);

            venda = new VendaModel
            {
                id = dt.Rows[0]["id"].ToString(),
                data = DateTime.Parse(dt.Rows[0]["data"].ToString()).ToString("dd/MM/yyyy"),
                total = double.Parse(dt.Rows[0]["total"].ToString()),
                vendedor_id = dt.Rows[0]["vendedor_id"].ToString(),
                cliente_id = dt.Rows[0]["cliente_id"].ToString(),
            };

            sql = $"SELECT id, nome, cpf, email, senha from Cliente where id='{venda.cliente_id}'";
            dt = objDAL.RetDataTable(sql);
            nome_cliente = dt.Rows[0]["nome"].ToString();
            venda.nome_cliente = nome_cliente;

            sql = $"select nome from vendedor where id= '{venda.vendedor_id}'";
            dt = objDAL.RetDataTable(sql);
            nomeVendedor = dt.Rows[0]["nome"].ToString();
            venda.nomeVendedor = nomeVendedor;
            return venda;
        }

        public List<VendaModel> retornarListaVendas(string DataDe, string DataAte)
        {
            {
                List<VendaModel> lista = new List<VendaModel>();
                VendaModel venda = new VendaModel();
                DAL objDAL = new DAL();
                string sql = "select v1.id, v1.data, v1.total, v2.nome as vendedor, c.nome as cliente from venda v1 inner" +
                     " join vendedor v2 on v1.vendedor_id = v2.id inner join cliente c on v1.cliente_id = c.id" +
                     $" where v1.data >='{DataDe}' and v1.data <='{DataAte}'order by data, total";
                DataTable dt = objDAL.RetDataTable(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    venda = new VendaModel()
                    {
                        id = dt.Rows[i]["id"].ToString(),
                        data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                        total = double.Parse(dt.Rows[i]["total"].ToString()),
                        vendedor_id = dt.Rows[i]["vendedor"].ToString(),
                        cliente_id = dt.Rows[i]["cliente"].ToString(),

                    };
                    lista.Add(venda);
                }
                return lista;
            }
        }


        public List<ClienteModel> retornarListaClientes()
        {
            return new ClienteModel().listarTodosClientes();
        }

        public List<VendedorModel> retornarListaVendedores()
        {
            return new VendedorModel().listarTodosVendedores();
        }

        public List<ProdutoModel> retornarListaProdutos()
        {
            return new ProdutoModel().listarTodosProdutos();
        }

        public List<ItemVendaModel> retornarItensVenda(int? id)
        {
            List<ItemVendaModel> listaProdutos = new List<ItemVendaModel>();
            ItemVendaModel venda = new ItemVendaModel();
            DAL objDAL = new DAL();
            string sql = $"SELECT venda_id, produto_id, qtde_produto, preco_produto, total_produto from itens_venda where venda_id = {id}";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                venda = new ItemVendaModel()
                {
                    idVenda = dt.Rows[i]["venda_id"].ToString(),
                    codigoProduto = dt.Rows[i]["produto_id"].ToString(),
                    qtdeProduto = dt.Rows[i]["qtde_produto"].ToString(),
                    precoUnitario = dt.Rows[i]["preco_produto"].ToString(),
                    total = dt.Rows[i]["total_produto"].ToString()
                };
                listaProdutos.Add(venda);

            }
            for (int i = 0; i < listaProdutos.Count; i++)
            {
                sql = $"select nome from produto where id = {listaProdutos[i].codigoProduto}";
                dt = objDAL.RetDataTable(sql);
                listaProdutos[i].nomeProduto = dt.Rows[0]["nome"].ToString();
            }
            return listaProdutos;
        }
        public void inserir()
        {
            DAL objDAL = new DAL();
            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");
            string sql = "INSERT INTO Venda (data, total, vendedor_id, cliente_id)" +
                $"VALUES('{dataVenda}', {total.ToString().Replace(",", ".")}, {vendedor_id}, {cliente_id})";
            objDAL.executarComandoSql(sql);

            //recuperar id de venda
            sql = $"SELECT id FROM Venda WHERE data='{dataVenda}' and vendedor_id={vendedor_id} and cliente_id={cliente_id} order by id desc limit 1";
            DataTable dt = objDAL.RetDataTable(sql);
            string idVenda = dt.Rows[0]["id"].ToString();

            //Deserializar o JSON da lista de produtos selecionados e grava-los na tabela itens_venda
            List<ItemVendaModel> ListaItens = JsonConvert.DeserializeObject<List<ItemVendaModel>>(listaProdutos);
            for (int i = 0; i < ListaItens.Count; i++)
            {
                sql = "insert into itens_venda (venda_id, produto_id, qtde_produto, preco_produto, total_produto)" +
                    $"values({idVenda}, {ListaItens[i].codigoProduto.ToString()}, {ListaItens[i].qtdeProduto.ToString()}," +
                    $"{ListaItens[i].precoUnitario.ToString().Replace(",", ".")}, {ListaItens[i].total.ToString().Replace(",", ".")})";
                objDAL.executarComandoSql(sql);

                //atualiza a quantidade de produtos em estoque
                sql = $"insert into saida_produto (id_produto, qtde, valor_unitario, data_saida)values({ListaItens[i].codigoProduto.ToString()}, {ListaItens[i].qtdeProduto.ToString()}," +
                      $"{ListaItens[i].precoUnitario.ToString().Replace(",", ".")}, '{dataVenda}')";
                objDAL.executarComandoSql(sql);

                
            }

        }

    }
}
