using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Modelo_Conexao_Banco
{
    class Program
    {
        static void Main(string[] args)
        {
            string nome = "Felipe";
            //Console.WriteLine("Hello World!");

            //Cria a conexao com o banco de dados SQL Server de acordo com a string de conexao mencionada no arquivo App/Web.config
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["localhost_connection"].ConnectionString);
            sqlConnection.Open();

            try
            {
                //Instanciando a classe responsavel pelo comando ja com query no seu construtor
                SqlCommand command = new SqlCommand(" SELECT * FROM tb_usuario WHERE nome = @nome");

                //A query também pode ser mencionada posteriormente alimentando a propriedade "CommandText", a consulta ira priorizar o valor dessa propriedade
                //command.CommandText = " SELECT * FROM tb_usuario WHERE id = @id";

                //Menciona qual é a conexao que o comando usara
                command.Connection = sqlConnection;


                command.Parameters.AddWithValue("@nome", nome);
                //command.Parameters.AddWithValue("@id", 2);

                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read()) {

                    Console.WriteLine("ID: " + Convert.ToInt32(dr["id"]));
                    Console.WriteLine("Nome: " + dr["nome"].ToString());
                    Console.WriteLine("Sobrenome: " + dr["sobrenome"].ToString());
                    Console.WriteLine("Idade: " + Convert.ToInt32(dr["idade"]));
                };
                
                //Limpeza DataReader
                dr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
