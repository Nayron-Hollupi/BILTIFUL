using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Entidades
{
    public class Fornecedor
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Fornecedor()
        {
        }

        public Fornecedor(string cnpj, string rsocial, DateTime dabertura)
        {
            this.CNPJ = cnpj;
            this.RazaoSocial = rsocial;
            this.DataAbertura = dabertura;
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


        bool comparador = true;
        public bool VerificaCNPJ(string cnpj)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {

                connection.Open();

                String sql = "SELECT CNPJ  FROM dbo.Fornecedor Where  CNPJ = " + cnpj;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetString(0) != null)
                            {
                                comparador = false;
                            }

                        }
                    }

                }
                connection.Close();

            }

            return comparador;
        }


        public void Inserir_Fornecedor(Fornecedor fornecedor)
        {

            SqlConnection connection = new SqlConnection(connString);
    
            if (comparador == true)
            {

                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Fornecedor", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = fornecedor.CNPJ;
                    sql_cmnd.Parameters.AddWithValue("@Razao_Social", SqlDbType.NVarChar).Value = fornecedor.RazaoSocial;
                    sql_cmnd.Parameters.AddWithValue("@Data_Abertura", SqlDbType.Date).Value = fornecedor.DataAbertura;
                    sql_cmnd.ExecuteNonQuery();

                    connection.Close();


                }
            }

        }

        public void MostrarFornecedor()
        {
            Console.WriteLine("\n\t\t\t\t\t          Dados dos Fornecedores  :");
            Console.WriteLine("\t\t\t\t\t=========================================");


            SqlConnection connection = new SqlConnection(connString);

            connection.Open();

            String sql = "SELECT   CNPJ , Razao_Social , Data_Abertura , Ultima_Compra ,Data_Cadastro , Situacao FROM dbo.Fornecedor";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Razao Social:  {1}   \n\t\t\t\t\t|CNPJ: {0} " +
                            "\n\t\t\t\t\t|Data de abertura: {2}  \n\t\t\t\t\t|Ultima Compra: {3} \n\t\t\t\t\t|Data Cadastro : {4}   \n\t\t\t\t\t|Situacao : {5} \n", reader.GetString(0), reader.GetString(1)
                             , reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetString(5));
                    }
                }

            }
            connection.Close();

        }

        public bool VerificaBloqueado(string cnpj)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {

                connection.Open();

                String sql = "SELECT CNPJ  FROM dbo.Bloqueado Where  CNPJ = " + cnpj;

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetString(0) != null)
                            {
                                comparador = false;
                            }

                        }
                    }

                }
                connection.Close();

            }

            return comparador;
        }

        public void Bloqueado(string cnpj)
        {
            SqlConnection connection = new SqlConnection(connString);
            if (comparador == true)
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Bloqueado", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;

                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();

                }
            }

        }

        public void RemoveBloqueado(string cnpj)
        {
            SqlConnection connection = new SqlConnection(connString);
            if (comparador == false)
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Remove_Bloqueado", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CNPJ", SqlDbType.NVarChar).Value = cnpj;

                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();

                }
            }

        }

        public void LocalizarFornecedor(string cnpj)
        {
            SqlConnection connection = new SqlConnection(connString);
            Console.WriteLine("\n\t\t\t\t\t         Dados do Fornecedor :");
            Console.WriteLine("\t\t\t\t\t=========================================");


            connection.Open();

            String sql = "SELECT CNPJ , Razao_Social , Data_Abertura , Ultima_Compra ,Data_Cadastro , Situacao  FROM dbo.Fornecedor Where  CNPJ = " + cnpj;

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Razao Social:  {1}   \n\t\t\t\t\t|CNPJ: {0} " +
                      "\n\t\t\t\t\t|Data de abertura: {2}  \n\t\t\t\t\t|Ultima Compra: {3} \n\t\t\t\t\t|Data Cadastro : {4}   \n\t\t\t\t\t|Situacao : {5} \n", reader.GetString(0), reader.GetString(1)
                       , reader.GetDateTime(2).ToString("dd/MM/yyyy"), reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetString(5));
                    }
                }

            }
            connection.Close();

        }


    }
}
