using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Namespace's que contém as classes que manipulam dados
using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
    public class AcessoDadosSqlServer
    {
        //Criar a conexão
        private SqlConnection criarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        //Parâmetros que vão para o banco
        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void limparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void adicionarParametros(string nomeParametro, object valorParametro)
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParametro, valorParametro));
        }

        //Persistência - Inserir, Alterar e Excluir
        public object executarManipulacao(CommandType commandType, string nomeStoredProdecureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = criarConexao();

                //Abrir a conexão
                sqlConnection.Open();

                //Crir o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                //Colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProdecureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Esse tem é em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Executar o comando, ou seja, mandar o comando ir até o banco de dados
                return sqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
        
        //Consultar registros do banco de dados
        public DataTable executarConsulta(CommandType commandType, string nomeStoredProdecureOuTextoSql)
        {
            try
            {
                //Criar a conexão
                SqlConnection sqlConnection = criarConexao();

                //Abrir a conexão
                sqlConnection.Open();

                //Criar o comando que vai levar a informação para o banco
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                //Colocando as coisas dentro do comando (dentro da caixa que vai trafegar na conexão)
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProdecureOuTextoSql;
                sqlCommand.CommandTimeout = 7200; //Esse tem é em segundos

                //Adicionar os parâmetros no comando
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                //Criar um adaptador
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

                //DataTable = Tabela de Dados vazia onde vai ser colocado os dados que vem do banco
                DataTable dataTable = new DataTable();

                //Mandar o comando ir até o banco buscar os dados e o adaptador preencher o datatable
                sqlDataAdapter.Fill(dataTable);

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
