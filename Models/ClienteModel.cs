using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//importação da classe DAL para fazer uso do bd
using SistemaVendas.Uteis;
//importar System.Data para trabalhar com dataTable
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    public class ClienteModel
    {
        public string id { get; set; }

        [Required(ErrorMessage= "Informe o Nome do cliente")]
        public string nome { get; set; }

        [Required(ErrorMessage ="Informe o CPF do cliente")]
        public string cpf { get; set; }

        [Required(ErrorMessage ="Informe o Email do cliente")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="O Email informado é invalido!")]
        public string email { get; set; }
        public string senha { get; set; }

        public List<ClienteModel> listarTodosClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = "SELECT id, nome, cpf, email, senha FROM Cliente ORDER BY nome asc";
            DataTable dt = objDAL.RetDataTable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ClienteModel
                {
                    id = dt.Rows[i]["id"].ToString(),
                    nome = dt.Rows[i]["nome"].ToString(),
                    cpf = dt.Rows[i]["cpf"].ToString(),
                    email = dt.Rows[i]["email"].ToString(),
                    senha = dt.Rows[i]["senha"].ToString()
                };
                lista.Add(item);

            }
            return lista;
        }

        public ClienteModel retornarCliente(int? id)
        {
            ClienteModel item;
            DAL objDAL = new DAL();
            string sql = $"SELECT id, nome, cpf, email, senha FROM Cliente WHERE id='{id}' ORDER BY id asc";
            DataTable dt = objDAL.RetDataTable(sql);

                item = new ClienteModel
                {
                    id = dt.Rows[0]["id"].ToString(),
                    nome = dt.Rows[0]["nome"].ToString(),
                    cpf = dt.Rows[0]["cpf"].ToString(),
                    email = dt.Rows[0]["email"].ToString(),
                    senha = dt.Rows[0]["senha"].ToString()
                };          
            return item;
        }

        public void gravar()
        {
            DAL objDAL = new DAL();
            string sql = string.Empty;
            if(id != null)
            {
                 sql = $"UPDATE Cliente SET nome='{nome}',cpf='{cpf}',email='{email}' where id = '{id}'";
            }
            else
            {
                 sql = $"INSERT INTO Cliente(nome, cpf, email, senha)" +
                $"values('{nome}','{cpf}','{email}','123')";
            }
            objDAL.executarComandoSql(sql);
        }

        //DELETE
        public void excluir(int id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM Cliente WHERE ID='{id}'";
            objDAL.executarComandoSql(sql);
        }

    }
}
