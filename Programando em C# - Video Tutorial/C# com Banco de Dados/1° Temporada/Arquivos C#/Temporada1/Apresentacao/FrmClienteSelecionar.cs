using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocios;
using ObjetoTransferencia;

namespace Apresentacao
{
    public partial class FrmClienteSelecionar : Form
    {
        public FrmClienteSelecionar()
        {
            InitializeComponent();

            //Não gerar linhas automaticamente
            dataGridViewPrincipal.AutoGenerateColumns = false;
        }

        private void buttonPesquisar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            ClienteNegocios clienteNegocios = new ClienteNegocios();
            ClienteColecao clienteColecao = new ClienteColecao();

            clienteColecao = clienteNegocios.ConsultarPorNome(textBoxPesquisa.Text);

            dataGridViewPrincipal.DataSource = null;
            dataGridViewPrincipal.DataSource = clienteColecao;

            dataGridViewPrincipal.Update();
            dataGridViewPrincipal.Refresh();
        }

        private void buttonFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            //Verificar se tem aglum registro selecionado
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");
                return;
            }

            //Perguntar se realmente quer excluir
            DialogResult resultado = MessageBox.Show("Tem certeza?","Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.No)
            {
                return;
            }

            //Pegar o cliente selecionado no grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);

            //Instânciar a regra de negócio
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            //Chama o método excluir
            string retorno = clienteNegocios.Excluir(clienteSelecionado);

            //Verificar se exluiu com sucesso
            //Se o retorno for um número é porque deu certo, senão é uma mensagem de erro
            try
            {
                int idCliente = Convert.ToInt32(retorno);
                MessageBox.Show("Cliente exluido com sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AtualizarGrid();
            }
            catch
            {
                MessageBox.Show("Não foi possível excluir.Detalhes: " + retorno,"Erro",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void buttonInserir_Click(object sender, EventArgs e)
        {
            //Instanciar o formulário de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Inserir, null);
            DialogResult dialogoResult = frmClienteCadastrar.ShowDialog();

            if (dialogoResult == DialogResult.Yes)
                AtualizarGrid();

        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            //Verificar se tem aglum registro selecionado
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");
                return;
            }
            
            //Pegar o cliente selecionado no grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);

            //Instânciar a regra de negócio
            ClienteNegocios clienteNegocios = new ClienteNegocios();

            //Instanciar o formulário de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Alterar, clienteSelecionado);

            DialogResult resultado = frmClienteCadastrar.ShowDialog();

            if (resultado == DialogResult.Yes)
                AtualizarGrid();

        }

        private void buttonConsultar_Click(object sender, EventArgs e)
        {
            //Verificar se tem aglum registro selecionado
            if (dataGridViewPrincipal.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhum cliente selecionado.");
                return;
            }

            //Pegar o cliente selecionado no grid
            Cliente clienteSelecionado = (dataGridViewPrincipal.SelectedRows[0].DataBoundItem as Cliente);

            //Instanciar o formulário de cadastro
            FrmClienteCadastrar frmClienteCadastrar = new FrmClienteCadastrar(AcaoNaTela.Consultar, clienteSelecionado);
            frmClienteCadastrar.ShowDialog();
        }
    }
}
