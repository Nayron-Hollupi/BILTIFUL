using BILTIFUL.Core.Entidades.Enums;
using System;
using System.Data;
using System.Data.SqlClient;



namespace BILTIFUL.Core.Entidades
{
    public class Cliente
    {

        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public DateTime UltimaCompra { get; set; } = DateTime.Now;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public Cliente()
        {
        }

        public Cliente(string cpf, string nome, DateTime dnascimento, Sexo sexo)
        {
            this.CPF = cpf;
            this.Nome = nome;
            this.DataNascimento = dnascimento;
            this.Sexo = sexo;
            this.Situacao = Situacao;
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
        public bool VerificaCPF(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
                
                connection.Open();

                String sql = "SELECT CPF  FROM dbo.Cliente Where  CPF = '" + cpf +"'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if ( reader.GetString(0) != null)
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

        public void Inserir_Cliente(Cliente cliente)
        {
            SqlConnection connection = new SqlConnection(connString);
            if (comparador == true)
            {

                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Cliente", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cliente.CPF;
                    sql_cmnd.Parameters.AddWithValue("@Nome", SqlDbType.NVarChar).Value = cliente.Nome;
                    sql_cmnd.Parameters.AddWithValue("@Data_Nascimento", SqlDbType.Date).Value = cliente.DataNascimento;
                    sql_cmnd.Parameters.AddWithValue("@Sexo", SqlDbType.Char).Value = (char)cliente.Sexo;
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();

                }
            }
               
        }

        public void MostrarCliente()
        {
            Console.WriteLine("\n\t\t\t\t\t         Dados dos Clientes  :");
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
                             , reader.GetString(3), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetDateTime(5).ToString("dd/MM/yyyy"), reader.GetString(6));
                    }
                }

            }
            connection.Close();

        }

        public void LocalizarCliente(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
           
            Console.WriteLine("\n\t\t\t\t\t         Dados do Cliente  :");
            Console.WriteLine("\t\t\t\t\t=========================================");
           
              
                connection.Open();

                String sql = "SELECT CPF , Nome ,Data_Nascimento , Sexo ,  Ultima_Compra , Data_Cadastro , Situacao    FROM dbo.Cliente Where  CPf = '" + cpf + "'";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Console.WriteLine(" \t\t\t\t\t -------------------------------------------\n\t\t\t\t\t|Nome:  {1}   \n\t\t\t\t\t|CPF: {0} " +
                                "\n\t\t\t\t\t|Data de nascimento: {2}  \n\t\t\t\t\t|Sexo: {3} \n\t\t\t\t\t|Ultima compra: {4}   \n\t\t\t\t\t|Data de cadastro: {5} \n\t\t\t\t\t|Situação: {6} \n", reader.GetString(0), reader.GetString(1), reader.GetDateTime(2).ToString("dd/MM/yyyy")
                                 , reader.GetString(3), reader.GetDateTime(4).ToString("dd/MM/yyyy"), reader.GetDateTime(5).ToString("dd/MM/yyyy"), reader.GetString(6));
                        }
                    }

                }
                connection.Close();
            
        }


        public bool VerificaRisco(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {

                connection.Open();

                String sql = "SELECT CPF  FROM dbo.Risco Where  CPF = " + cpf;

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


        public void Risco(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
            if (comparador == true)
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Inserir_Risco", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;
                   
                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();

                }
            }

        }

        public void RemoveRisco(string cpf)
        {
            SqlConnection connection = new SqlConnection(connString);
            if (comparador == false)
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand sql_cmnd = new SqlCommand("Remove_Risco", connection);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@CPF", SqlDbType.NVarChar).Value = cpf;

                    sql_cmnd.ExecuteNonQuery();
                    connection.Close();

                }
            }

        }

    }

}
