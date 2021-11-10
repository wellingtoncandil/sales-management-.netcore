using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class ProdutoModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto!")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição do produto!")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço unitário!")]
        public decimal? preco_unitario { get; set; }

        //o sinal de interrogação faz com que seja permitido o valor nulo para decimal
        //fazendo assim com que o dataNotation funcione, caso contrario seria utilizado por padrão o valor 0
        [Required(ErrorMessage = "Informe a quantidade a ser inserida!")]
        public decimal? quantidade_estoque { get; set; }
        
        [Required(ErrorMessage = "Informe a unidade de medida do produto!")]
        public string unidade_medida { get; set; }

        [Required(ErrorMessage = "Informe o link da imagem do produto!")]
        public string link_foto { get; set; }

        public List<ProdutoModel> listarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel produto = new ProdutoModel();
            DAL objDAL = new DAL();
            string sql = "SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM Produto ORDER BY nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                produto = new ProdutoModel()
                {
                    id = dt.Rows[i]["id"].ToString(),
                    nome = dt.Rows[i]["nome"].ToString(),
                    descricao = dt.Rows[i]["descricao"].ToString(),
                    preco_unitario = decimal.Parse(dt.Rows[i]["preco_unitario"].ToString()),
                    quantidade_estoque = decimal.Parse(dt.Rows[i]["quantidade_estoque"].ToString()),
                    unidade_medida = dt.Rows[i]["unidade_medida"].ToString(),
                    link_foto = dt.Rows[i]["link_foto"].ToString()
                };
                lista.Add(produto);
            }
            return lista;
        }

        public ProdutoModel retornarProduto(int? id)
        {
            ProdutoModel produto;
            DAL objDAL = new DAL();
            string sql = $"SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM Produto WHERE id='{id}' ORDER BY id asc";
            DataTable dt = objDAL.RetDataTable(sql);

            produto = new ProdutoModel
            {
                id = dt.Rows[0]["id"].ToString(),
                nome = dt.Rows[0]["nome"].ToString(),
                descricao = dt.Rows[0]["descricao"].ToString(),
                preco_unitario = decimal.Parse(dt.Rows[0]["preco_unitario"].ToString()),
                quantidade_estoque = decimal.Parse(dt.Rows[0]["quantidade_estoque"].ToString()),
                unidade_medida = dt.Rows[0]["unidade_medida"].ToString(),
                link_foto = dt.Rows[0]["link_foto"].ToString()
            };
            return produto;
        }

        public void gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;
            if(id != null)
            {
                // necessário fazer o replace nos numeros decimais para que grave os valores corretamente no banco de dados
                sql = $"UPDATE Produto SET nome='{nome}', descricao='{descricao}', preco_unitario={preco_unitario.ToString().Replace(",",".")}, quantidade_estoque={quantidade_estoque.ToString().Replace(",", ".")}, " +
                    $"unidade_medida='{unidade_medida}', link_foto='{link_foto}' where id = '{id}'";
            }
            else
            {
                sql = $"SET AUTOCOMMIT = 0;" +
                    $"START TRANSACTION;" +
                    $"INSERT INTO Produto(nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto)" +
                    $"values('{nome}','{descricao}',{preco_unitario.ToString().Replace(",", ".")},{quantidade_estoque.ToString().Replace(",", ".")},'{unidade_medida}','{link_foto}');" +
                    "INSERT INTO estoque(id_produto, qtde, valor_unitario )" +
                    "values((select LAST_INSERT_ID()),'','' );" +
                    "COMMIT;" +
                    "SET AUTOCOMMIT = 1;";
            }
            objDAL.executarComandoSql(sql);
        }

        public void excluirEstoque(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM estoque WHERE id_produto='{id}'";
            objDAL.executarComandoSql(sql);
            excluir(id);
        }

        public void excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM Produto WHERE ID='{id}'";
            objDAL.executarComandoSql(sql);
        }
    }
}

                    