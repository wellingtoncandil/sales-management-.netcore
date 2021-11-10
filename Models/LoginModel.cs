using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
//necessário para criar o Required()
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public string id { get; set; }

        public string nome { get; set; }

        //mensagem de erro ao tentar logar sem ter preenchido o campo email
        [Required(ErrorMessage="Informe o email!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="O email informado é invalido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha!")]
        public string senha { get; set; }

 
        public Boolean validarLogin()
        {
            string sql = $"SELECT id, nome FROM Vendedor WHERE email =@email AND senha =@senha";
            MySqlCommand command = new MySqlCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@senha", senha);

            DAL objDAL = new DAL();
            
            DataTable dt = objDAL.RetDataTable(command);
            if(dt.Rows.Count == 1)
            {
                //guardando o id e o nome do usuario
                id = dt.Rows[0]["id"].ToString();
                nome = dt.Rows[0]["nome"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
