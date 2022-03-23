using BILTIFUL.Core.Entidades.Base;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Entidades
{
    public class Producao : EntidadeBase
    {
        public DateTime DataProducao { get; set; } = DateTime.Now;
        //ID produto
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }

        public Producao()
        {
        }

        public Producao(string id, string produto, decimal quantidade)
        {
            Id = id;
            Produto = produto;
            Quantidade = quantidade;
        }

        public Producao(string produto, decimal quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }

        public Producao(string id,DateTime dataProducao, string produto, decimal quantidade)
        {
            Id = id;
            DataProducao = dataProducao;
            Produto = produto;
            Quantidade = quantidade;
        }



        public static string datasource = @"DESKTOP-4J7NJHL";//instancia do servidor
        public static string database = "BILTIFUL"; //Base de Dados
        public static string username = "sa"; //usuario da conexão
        public static string password = "123456"; //senha

        //sua string de conexão 
        static string connString = @"Data Source=" + datasource + ";Initial Catalog="
                     + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

        //cria a instância de conexão com a base de dados
        SqlConnection connection = new SqlConnection(connString);


        public void Inserir_Producao(Producao producao)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("Inserir_Producao", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue(" @Codigo_Barras_Produto", SqlDbType.NVarChar).Value = producao.Produto;
                sql_cmnd.Parameters.AddWithValue(" @Quantidade", SqlDbType.Decimal).Value = producao.Quantidade;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();


            }

        }
    }
}
