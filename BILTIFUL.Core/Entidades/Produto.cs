using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Entidades
{
    public class Produto
    {
        public string CodigoBarras { get; set; } = "7896617";
        public string Nome { get; set; }
        public Decimal ValorVenda { get; set; }
        public DateTime UltimaVenda { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Produto()
        {
        }


        public Produto(string cbarras, string nome, decimal valorvenda)
        {
            this.CodigoBarras += cbarras.PadLeft(5,'0');
            this.Nome = nome;
            this.ValorVenda = valorvenda;
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
        public void Inserir_Produto(Produto produto)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
               
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Produto", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@Codigo_Barras", SqlDbType.NVarChar).Value = produto.CodigoBarras;
                    sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = produto.Nome;
                    sql_cmnd.Parameters.AddWithValue("@Valor_Venda", SqlDbType.Decimal).Value = produto.ValorVenda;
                    sql_cmnd.ExecuteNonQuery();               
                    connection.Close();

            }

        }

        public void MostrarProduto()
        {

            SqlConnection connection = new SqlConnection(connString);
            Console.WriteLine("\n\t\t\t\t\t           Produtos  :");
            Console.WriteLine("\t\t\t\t\t=========================================");




            connection.Open();

            String sql = "SELECT  Codigo_Barras ,Nome , Valor_Venda , Ultima_Venda , Data_Cadastro , Situacao  FROM dbo.Produto";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Nome:  {1}   \n\t\t\t\t\t|Codigo Barras: {0} " +
                            "\n\t\t\t\t\t|Valor da Venda: {2}  \n\t\t\t\t\t| Ultima Venda: {3} \n\t\t\t\t\t|Data do Cadastro: {4}   \n\t\t\t\t\t|Situacao: {5} \n", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2)
                             , reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetString(5));
                    }
                }

            }
            connection.Close();

        }

        public bool VerificaProduto(string nome)
        {

            bool comparador = true;
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {

                connection.Open();

                String sql = "SELECT Nome  FROM dbo.Produto  Where Nome = '" + nome + "'";

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

        public bool ProdutoCadastrado()
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {

                connection.Open();

                String sql = "SELECT Nome  FROM dbo.Produto  ";

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
        public void LocalizarProduto(string nome)
        {


            SqlConnection connection = new SqlConnection(connString);

            if (comparador == false)
            {
                connection.Open();

                String sql = "SELECT  Codigo_Barras ,Nome , Valor_Venda , Ultima_Venda , Data_Cadastro , Situacao  FROM dbo.Produto   WHERE  Nome = '" + nome + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Nome:  {1}   \n\t\t\t\t\t|Codigo Barras: {0} " +
                            "\n\t\t\t\t\t|Valor da Venda: {2}  \n\t\t\t\t\t| Ultima Venda: {3} \n\t\t\t\t\t|Data do Cadastro: {4}   \n\t\t\t\t\t|Situacao: {5} \n", reader.GetString(0), reader.GetString(1), reader.GetDecimal(2)
                             , reader.GetDateTime(3).ToString("dd/MM/yyyy"), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetString(5));
                        }
                    }

                }
                connection.Close();
            }
        }
    }
}