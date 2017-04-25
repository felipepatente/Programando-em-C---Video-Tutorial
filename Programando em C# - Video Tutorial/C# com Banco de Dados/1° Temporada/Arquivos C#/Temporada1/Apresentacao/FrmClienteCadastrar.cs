using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjetoTransferencia;
using Negocios;

namespace Apresentacao
{
    public partial class FrmClienteCadastrar : Form
    {
        AcaoNaTela acaoNaTelaSelecionada;

        public FrmClienteCadastrar(AcaoNaTela acaoNaTela, Cliente cliente)
        {
            InitializeComponent();

            this.acaoNaTelaSelecionada = acaoNaTela;

            if (acaoNaTela == AcaoNaTela.Inserir)
            {
                this.Text = "Inserir Cliente";
            }else if (acaoNaTela == AcaoNaTela.Alterar)
            {
                this.Text = "Alterar Cliente";
                textBoxCodigo.Text = cliente.idCliente.ToString();
                textBoxNome.Text = cliente.nome;
                dateDataNascimento.Value = cliente.dataNasimento;

                if (cliente.sexo == true)//Masculino                
                    radioSexoMasculino.Checked = true;
                else                
                    radioSexoFeminino.Checked = true;
                

                textBoxLimiteCompra.Text = cliente.limiteCompra.ToString();
            }
            else if (acaoNaTela == AcaoNaTela.Consultar)
            {
                this.Text = "Consultar Cliente";

                //Carregar campos da tela
                textBoxCodigo.Text = cliente.idCliente.ToString();
                textBoxNome.Text = cliente.nome;
                dateDataNascimento.Value = cliente.dataNasimento;

                if (cliente.sexo == true)//Masculino                
                    radioSexoMasculino.Checked = true;
                else
                    radioSexoFeminino.Checked = true;

                textBoxLimiteCompra.Text = cliente.limiteCompra.ToString();

                //Desabilitar campos da tela
                textBoxNome.ReadOnly = true;
                textBoxNome.TabStop = false;
                dateDataNascimento.Enabled = false;
                radioSexoMasculino.Enabled = false;
                radioSexoFeminino.Enabled = false;
                textBoxLimiteCompra.ReadOnly = true;
                textBoxLimiteCompra.TabStop = false;

                buttonSalvar.Visible = false;
                buttonCancelar.Text = "Fechar";
                buttonCancelar.Focus();


            }
            
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            //Verificar se é inserção ou alteração
            if (acaoNaTelaSelecionada == AcaoNaTela.Inserir)
            {
                Cliente cliente = new Cliente();
                cliente.nome = textBoxNome.Text;
                cliente.dataNasimento = dateDataNascimento.Value;

                if (radioSexoMasculino.Checked == true)
                    cliente.sexo = true;
                else
                    cliente.sexo = false;
                
                cliente.limiteCompra = Convert.ToDecimal(textBoxLimiteCompra.Text);

                ClienteNegocios clienteNegocios = new ClienteNegocios();
                string retorno = clienteNegocios.Inserir(cliente);

                try
                {
                    int idCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente inserindo com sucesso. Código: " + idCliente);
                    this.DialogResult = DialogResult.Yes;
                }
                catch
                {
                    MessageBox.Show("Não foi possível inserir.Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
            else if(acaoNaTelaSelecionada == AcaoNaTela.Alterar)
            {
                Cliente cliente = new Cliente();

                cliente.idCliente = Convert.ToInt32(textBoxCodigo.Text);
                cliente.nome = textBoxNome.Text;
                cliente.dataNasimento = dateDataNascimento.Value;

                if (radioSexoMasculino.Checked == true)
                    cliente.sexo = true;
                else
                    cliente.sexo = false;

                cliente.limiteCompra = Convert.ToDecimal(textBoxLimiteCompra.Text);

                ClienteNegocios clienteNegocios = new ClienteNegocios();
                string retorno = clienteNegocios.Alterar(cliente);

                try
                {
                    int idCliente = Convert.ToInt32(retorno);
                    MessageBox.Show("Cliente alterado com sucesso. Código: " + idCliente);
                    this.DialogResult = DialogResult.Yes;
                }
                catch
                {
                    MessageBox.Show("Não foi possível alterar.Detalhes: " + retorno, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.No;
                }
            }
        }
    }
}
