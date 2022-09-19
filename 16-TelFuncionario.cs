using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monisePerso
{
    public partial class frmTelFuncionario : Form
    {
        public frmTelFuncionario()
        {
            InitializeComponent();
        }

        private void InserirFoneFuncionario()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO fonefuncionario(idFoneFuncionario,numeroFuncionario,operFoneFuncionario,descFoneFuncionario,idFuncionario)VALUES(DEFAULT,@numero,@operadora,@descricao,@codEmpresa)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroFuncionario);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneFuncionario);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneFuncionario);
                cmd.Parameters.AddWithValue("@codEmpresa", Variaveis.codFuncionario);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone do funcionario cadastrado com sucesso!", "CADASTRO DO TELEFONE DO FUNCIONARIO");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone do funcionario!\n\n" + ex.Message, "Erro.");
            }
        }

        private void AlterarFoneFuncionario() //ALTERAR EM telEmpresa
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE fonefuncionario SET idFoneFuncionario=@codFone,numeroFuncionario=@numero,operFoneFuncionario=@operadora,descFoneFuncionario=@descricao WHERE idFuncionario=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneFuncionario);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroFuncionario);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneFuncionario);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneFuncionario);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codFuncionario);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone do funcionario alterado com sucesso!", "ALTERAÇÃO DO TELEFONE DO FUNCIONARIO");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar o telefone do funcionario!\n\n" + ex, "Erro.");
            }
        }

        private void CarregarFuncionario() //CARREGAR EM telFuncionario
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idFuncionario,nomeFuncionario FROM funcionario ORDER BY nomeFuncionario";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbFuncionarios.DataSource = dt;
                cmbFuncionarios.DisplayMember = "nomeFuncionario";
                cmbFuncionarios.ValueMember = "idFuncionario";
                cmbFuncionarios.SelectedIndex = -1;
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar a lista de funcionario\n\n" + erro.Message, "ERRO!");
            }
        }

        private void CarregarDadosFoneFuncionario()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idFoneFuncionario,numeroFuncionario,operFoneFuncionario,descFoneFuncionario,nomeFuncionario FROM fonefuncionario INNER JOIN funcionario ON fonefuncionario.idFuncionario = funcionario.idFuncionario WHERE idFoneFuncionario=@codFone";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneFuncionario);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.numeroFuncionario = reader.GetString(1);
                    Variaveis.operFoneFuncionario = reader.GetString(2);
                    Variaveis.descFoneFuncionario = reader.GetString(3);
                    Variaveis.nomeFuncionario = reader.GetString(4);
                    txtCodigo.Text = Variaveis.numeroFuncionario.ToString();
                    txtCodigo.Text = Variaveis.operFoneFuncionario;
                    txtCodigo.Text = Variaveis.descFoneFuncionario;
                    txtCodigo.Text = Variaveis.nomeFuncionario;
                }
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados de telefone do funcionario!\n\n" + ex.Message, "ERRO");
            }
        }

        private void pctFechar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja encerrar?", "Encerrar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                new frmLogin().Show();
                Close();
            }
        }

        private void lblSair_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                new frmLogin().Show();
                Close();
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                new frmLogin().Show();
                Close();
            }
        }

        private void frmTelFuncionario_Load(object sender, EventArgs e)
        {
            pnlTelFuncionario.Location = new Point(this.Width / 2 - pnlTelFuncionario.Width / 2, this.Height / 2 - pnlTelFuncionario.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            CarregarFuncionario();
            cmbFuncionarios.Text = Variaveis.nomeFuncionario;

            if (Variaveis.funcao == "ALTERAR FONE")
            {
                CarregarDadosFoneFuncionario();
            }

            if (Variaveis.nivel != "ADMINISTRADOR")
            {
                lblEmpresa.Enabled = false;
                lblFuncionarios.Enabled = false;
                lblAplicativo.Enabled = false;
            }
            else
            {
                lblEmpresa.Enabled = true;
                lblFuncionarios.Enabled = true;
                lblAplicativo.Enabled = true;
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNomeFuncionario.ForeColor = Color.FromArgb(70, 10, 45);
            lblNumeroTelefone.ForeColor = Color.FromArgb(70, 10, 45);
            lblOperadora.ForeColor = Color.FromArgb(70, 10, 45);
            lblDescrição.ForeColor = Color.FromArgb(70, 10, 45);


            if (cmbFuncionarios.Text == String.Empty)
            {
                lblNomeFuncionario.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a empresa");
                cmbFuncionarios.Focus();
            }
            else if (mkdTel.Text == String.Empty)
            {
                lblNumeroTelefone.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o número da empresa");
                mkdTel.Focus();
            }
            else if (cmbOperadora.Text == String.Empty)
            {
                lblOperadora.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a operadora");
                cmbOperadora.Focus();
            }
            else if (txtDescricao.Text == String.Empty)
            {
                lblDescrição.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a descrição");
                txtDescricao.Focus();
            }
            else
            {
                Variaveis.nomeFuncionario = cmbFuncionarios.Text;
                Variaveis.numeroFuncionario = mkdTel.Text;
                Variaveis.operFoneFuncionario = cmbOperadora.Text;
                Variaveis.descFoneFuncionario = txtDescricao.Text;

                if (Variaveis.funcao == "CADASTRAR FONE")
                {
                    InserirFoneFuncionario();
                }
                else if (Variaveis.funcao == "ALTERAR FONE")
                {
                    AlterarFoneFuncionario();
                }

                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            cmbFuncionarios.SelectedIndex = -1;
            mkdTel.Clear();
            cmbOperadora.SelectedIndex = -1;
            txtDescricao.Clear();

            cmbFuncionarios.Focus();
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Close();
        }

        private void lblEncomendas_Click(object sender, EventArgs e)
        {
            //new frmEncomendas().Show();
            //Close();
        }

        private void lblClientes_Click(object sender, EventArgs e)
        {
            new frmClientes().Show();
            Close();
        }

        private void lblFuncionarios_Click(object sender, EventArgs e)
        {
            new frmFuncionarios().Show();
            Close();
        }

        private void lblEmpresa_Click(object sender, EventArgs e)
        {
            new frmEmpresa().Show();
            Close();
        }

        private void lblProdutos_Click(object sender, EventArgs e)
        {
            new frmProdutos().Show();
            Close();
        }

        private void lblSobre_Click(object sender, EventArgs e)
        {
            new frmSobre().Show();
            Close();
        }

        private void lblAplicativo_Click(object sender, EventArgs e)
        {
            new frmAplicativo().Show();
            Close();
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            new frmEmail().Show();
            Close();
        }
    }
}
