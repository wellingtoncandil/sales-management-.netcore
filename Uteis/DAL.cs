using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SistemaVendas.Uteis
{
    public class DAL
    {
        //necessário instalar a biblioteca do mysql no projeto. tools>Nuget Packet Manager> Manage for solution > perquisar por Mysql.data
        private static string Server = "localhost";
        private static string Database = "sistema_vendas";
        private static string User = "root";
        private static string Password = "";
        //string para conexão com o banco de dados
        private static string connectionString = $"Server={Server};DataBase={Database};Uid{User};Pwd={Password};Sslmode=none;Charset=utf8;";
        private static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(connectionString);
            //chama metodo que faz a conexão
            Connection.Open();
        }

        //espera um parametro do tipo string contendo um parametro do tipo sql para buscar info no bd
        public DataTable RetDataTable(string sql)
        {
            DataTable data = new DataTable();
            MySqlCommand command = new MySqlCommand(sql, Connection);
            //os dados que serao retornados não sao capazes de serem convertidos para datatable, é necessário
            //utilizar um "adaptador" mysqldataadapter
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            //metodo Fill dentro de MySqlDataAdapter capaz de preencher o dataTable
            da.Fill(data);
            return data;
        }
        public DataTable RetDataTable(MySqlCommand command)
        {
            DataTable data = new DataTable();
            command.Connection = Connection;
            MySqlDataAdapter da = new MySqlDataAdapter(command);
            da.Fill(data);
            return data;
        }

        //espera um parametro do tipo string contendo um parametro do tipo sql insert, uptade, delete
        public void executarComandoSql(string sql)
        {
            MySqlCommand command = new MySqlCommand(sql, Connection);
            command.ExecuteNonQuery();
        }
    }
}
