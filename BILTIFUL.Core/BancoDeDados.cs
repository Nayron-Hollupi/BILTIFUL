using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BILTIFUL.Core.Entidades;
using BILTIFUL.Core.Entidades.Enums;
using BILTIFUL.Core;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core
{
    public class BancoDeDados
    {


        public  static string datasource = @"DESKTOP-4J7NJHL";//instancia do servidor
        public  static string database = "BILTIFUL"; //Base de Dados
        public  static string username = "sa"; //usuario da conexão
        public  static string password = "123456"; //senha

        //sua string de conexão 
       static string connString = @"Data Source=" + datasource + ";Initial Catalog="
                    + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

        //cria a instância de conexão com a base de dados
        SqlConnection connection = new SqlConnection(connString);

        bool comparador = true;
        public bool VerificaCPF(string cpf)
        {
       
            using (connection)
            {
               

                String sql = "SELECT CPF  FROM dbo.Cliente";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string CPF = reader.GetString(0);
                            if (cpf == reader.GetString(0))
                            {
                                comparador = false;
                            }

                        }
                    }

                }
            }

            return comparador;
          }



        public void Inserir_Risco(Cliente cliente)
        {

            

            using (connection)
            {
                connection.Open();
                SqlCommand sql_cmnd = new SqlCommand("Inserir_Risco", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cliente.CPF;        
                    sql_cmnd.ExecuteNonQuery();       
                connection.Close();


            }

        }
    

            
            public void MostrarCliente()
        {
            Console.WriteLine("\n\t\t\t\t\t            Dados dos Clientes  :");
            Console.WriteLine("\t\t\t\t\t=========================================");
          



                connection.Open();

                String sql = "SELECT CPF , Nome ,Data_Nascimento , Sexo ,  Ultima_Compra , Data_Cadastro , Situacao    FROM dbo.Cliente";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
        
                            Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Nome:  {1}   \n\t\t\t\t\t|CPF: {0} " +
                                "\n\t\t\t\t\t|Data de nascimento: {2}  \n\t\t\t\t\t|Sexo: {3} \n\t\t\t\t\t|Ultima compra: {4}   \n\t\t\t\t\t|Data de cadastro: {5} \n\t\t\t\t\t|Situação: {6} \n", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy")
                                 , reader.GetString(3), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetDateTime(5).ToString("dd/MM/yyyy"), reader.GetString(6)) ;
                        }
                    }

                }
                connection.Close();
            
        }

    }
}