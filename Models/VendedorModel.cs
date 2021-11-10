using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class VendedorModel
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Informe o nome do vendedor!")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe o email do vendedor!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="O Email informado é invalido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha do vendedor!")]
        public string senha { get; set; }

        public List<VendedorModel> listarTodosVendedores()
        {
            List<VendedorModel> lista = new List<VendedorModel>();
            VendedorModel vendedor = new VendedorModel();
            DAL objDAL = new DAL();
            string sql = "SELECT id, nome, email, senha FROM Vendedor ORDER BY nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                vendedor = new VendedorModel()
                {
                    id = dt.Rows[i]["id"].ToString(),
                    nome = dt.Rows[i]["nome"].ToString(),
                    email = dt.Rows[i]["email"].ToString(),
                    senha = dt.Rows[i]["senha"].ToString()
                };
                lista.Add(vendedor);
            }
            return lista;
        }

        public VendedorModel retornarVendedor(int? id)
        {
            VendedorModel vendedor;
            DAL objDAL = new DAL();
            string sql = $"SELECT id, nome, email, senha FROM Vendedor WHERE id='{id}' ORDER BY id asc";
            DataTable dt = objDAL.RetDataTable(sql);

            vendedor = new VendedorModel
            {
                id = dt.Rows[0]["id"].ToString(),
                nome = dt.Rows[0]["nome"].ToString(),
                email = dt.Rows[0]["email"].ToString(),
                senha = dt.Rows[0]["senha"].ToString()
            };
            return vendedor;
        }

        public void gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;
            if(id != null)
            {
                sql = $"UPDATE Vendedor SET nome='{nome}',email='{email}', senha='{senha}' where id = '{id}'";
            }
            else
            {
                sql = $"INSERT INTO Vendedor(nome, email, senha)" +
                $"values('{nome}','{email}','{senha}')";
            }
            objDAL.executarComandoSql(sql);
        }

        public void excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM Vendedor WHERE ID='{id}'";
            objDAL.executarComandoSql(sql);
        }
    }
}
