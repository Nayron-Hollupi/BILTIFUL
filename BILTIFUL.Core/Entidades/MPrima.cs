using BILTIFUL.Core.Entidades.Base;
using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Entidades
{
    public class MPrima : EntidadeBase
    {
        public string Nome { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public MPrima()
        {
        }

        public MPrima(string idcod,string nome)
        {
            Id = "MP" + idcod.PadLeft(4, '0');
            this.Nome = nome;
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

     
        public void Inserir_Materia_Prima(MPrima mprima)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
                            connection.Open();

                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Materia_Prima", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Codigo", SqlDbType.NVarChar).Value = mprima.Id;
                    sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = mprima.Nome;
                    sql_cmnd.ExecuteNonQuery();
              
                   connection.Close();


            }

        }



        public void MostrarMateriaPrima()
        {
            Console.WriteLine("\n\t\t\t\t\t            Materia Prima :");
            Console.WriteLine("\t\t\t\t\t=========================================");

            SqlConnection connection = new SqlConnection(connString);


            connection.Open();

            String sql = "SELECT Codigo , Nome , Ultimo_Compra , Data_Cadastro ,Situacao   FROM dbo.Materia_Prima";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Nome:  {1}   \n\t\t\t\t\t|Codigo: {0} " +
                            "\n\t\t\t\t\t|Ultima compra: {2}  \n\t\t\t\t\t|Data Cadastro: {3} \n\t\t\t\t\tSituacao: {4}   \n", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy")
                             , reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetString(4));
                    }
                }

            }
            connection.Close();

        }


    }
}
