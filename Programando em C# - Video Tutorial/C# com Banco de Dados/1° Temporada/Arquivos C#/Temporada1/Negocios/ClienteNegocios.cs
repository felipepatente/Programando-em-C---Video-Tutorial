using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using AcessoBancoDados;
using ObjetoTransferencia;

namespace Negocios
{
    public class ClienteNegocios
    {
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.limparParametros();
                acessoDadosSqlServer.adicionarParametros("@Nome", cliente.nome);
                acessoDadosSqlServer.adicionarParametros("@DataNascimento", cliente.dataNasimento);
                acessoDadosSqlServer.adicionarParametros("@Sexo", cliente.sexo);
                acessoDadosSqlServer.adicionarParametros("@LimiteCompra", cliente.limiteCompra);
                string idCliente = acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();

                return idCliente;
            }
            catch(Exception exception)
            {
                return exception.Message;
            }
        }

        public string Alterar(Cliente cliente)
        {
            try {
                acessoDadosSqlServer.limparParametros();
                acessoDadosSqlServer.adicionarParametros("@IdCliente", cliente.idCliente);
                acessoDadosSqlServer.adicionarParametros("@Nome", cliente.nome);
                acessoDadosSqlServer.adicionarParametros("@DataNascimento", cliente.dataNasimento);
                acessoDadosSqlServer.adicionarParametros("@Sexo", cliente.sexo);
                acessoDadosSqlServer.adicionarParametros("@LimiteCompra", cliente.limiteCompra);
                string idCliente = acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();
                return idCliente;
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.limparParametros();
                acessoDadosSqlServer.adicionarParametros("@IdCliente", cliente.idCliente);
                string idCliente = acessoDadosSqlServer.executarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();
                return idCliente;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public ClienteColecao ConsultarPorNome(string nome)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();
                acessoDadosSqlServer.limparParametros();
                acessoDadosSqlServer.adicionarParametros("@Nome", nome);
                DataTable dataTableCliente = acessoDadosSqlServer.executarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                /* Percorrer o DataTable e transformar em coleção de cliente
                 * cada linha do DataTable é um cliente.
                 * Data = Dados e Row = Linha
                 */
                foreach (DataRow linha in dataTableCliente.Rows)
                {
                    /* Criar um cliente vazio colocar os dados na linha
                     * nele e adicionar na coleção
                     */
                    Cliente cliente = new Cliente();

                    cliente.idCliente = Convert.ToInt32(linha["IdCliente"]);
                    cliente.nome = Convert.ToString(linha["Nome"]);
                    cliente.dataNasimento = Convert.ToDateTime(linha["DataNascimento"]);
                    cliente.sexo = Convert.ToBoolean(linha["Sexo"]);
                    cliente.limiteCompra = Convert.ToDecimal(linha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar por nome. Detalhes: " + ex.Message);
            }
        }

        public ClienteColecao ConsultarPorId(int idCliente)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();
                acessoDadosSqlServer.limparParametros();
                acessoDadosSqlServer.adicionarParametros("@IdCliente",idCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.executarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow dataRowLinha in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();

                    cliente.idCliente = Convert.ToInt32(dataRowLinha["IdCliente"]);
                    cliente.nome = Convert.ToString(dataRowLinha["Nome"]);
                    cliente.dataNasimento = Convert.ToDateTime(dataRowLinha["DataNascimento"]);
                    cliente.sexo = Convert.ToBoolean(dataRowLinha["Sexo"]);
                    cliente.limiteCompra = Convert.ToDecimal(dataRowLinha["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível consultar por código. Detalhes: " + ex.Message);
            }
        }

    }
}
