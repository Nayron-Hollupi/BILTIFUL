using BILTIFUL.Core.Entidades.Base;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BILTIFUL.Core.Entidades
{
    public class ItemProducao : EntidadeBase
    {
        public DateTime DataProducao { get; set; } = DateTime.Now;
        //ID Materia Prima
        public string MateriaPrima { get; set; }
        public decimal QuantidadeMateriaPrima { get; set; }


        public ItemProducao()
        {
       

        }
        public ItemProducao(string materiaPrima, decimal quantidadeMateriaPrima)
        {
            this.MateriaPrima = materiaPrima;
            this.QuantidadeMateriaPrima = quantidadeMateriaPrima;

        }

        public ItemProducao(string id, DateTime dataProducao, string materiaPrima, decimal quantidadeMateriaPrima)
        {
            Id = id;
            DataProducao = dataProducao;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
        }

        public ItemProducao(string id,string materiaPrima, decimal quantidadeMateriaPrima)
        {
            Id = id;
            MateriaPrima = materiaPrima;
            QuantidadeMateriaPrima = quantidadeMateriaPrima;
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

     
        public void Inserir_Item_Producao(ItemProducao itemproducao)
        {
            SqlConnection connection = new SqlConnection(connString);
            using (connection)
            {
                connection.Open();

                SqlCommand sql_cmnd = new SqlCommand("Inserir_Item_Producao", connection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@Codigo_Materia_Prima", SqlDbType.NVarChar).Value = itemproducao.MateriaPrima;
                sql_cmnd.Parameters.AddWithValue("@Quantidade_Materia_Prima", SqlDbType.Decimal).Value = itemproducao.QuantidadeMateriaPrima;
                sql_cmnd.ExecuteNonQuery();

                connection.Close();


            }

        }
    }
}
