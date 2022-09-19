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
    public partial class frmTelEmpresa : Form
    {
        public frmTelEmpresa()
        {
            InitializeComponent();
        }

        private void InserirFoneEmpresa()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO foneempresa(idFoneEmpresa,numeroEmpresa,operFoneEmpresa,descFoneEmpresa,idEmpresa)VALUES(DEFAULT,@numero,@operadora,@descricao,@codEmpresa)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroEmpresa);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneEmpresa);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneEmpresa);
                cmd.Parameters.AddWithValue("@codEmpresa", Variaveis.codEmpresa);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone da empresa cadastrada com sucesso!", "CADASTRO DO TELEFONE DA EMPRESA");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone da empresa!\n\n" + ex.Message, "Erro.");
            }
        }

        private void AlterarFoneEmpresa() //ALTERAR EM telEmpresa
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE foneempresa SET idFoneEmpresa=@codFone,numeroEmpresa=@numero,operFoneEmpresa=@operadora,descFoneEmpresa=@descricao";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroEmpresa);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneEmpresa);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneEmpresa);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneEmpresa);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone da empresa alterada com sucesso!", "ALTERAÇÃO DO TELEFONE DA EMPRESA");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone da empresa!\n\n" + ex.Message, "Erro.");
            }
        }

        private void CarregarEmpresas() //CARREGAR EM telEmpresa
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idEmpresa,nomeEmpresa FROM empresa ORDER BY nomeEmpresa";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbEmpresa.DataSource = dt;
                cmbEmpresa.DisplayMember = "nomeEmpresa";
                cmbEmpresa.ValueMember = "idEmpresa";
                cmbEmpresa.SelectedIndex = -1;
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar a lista de empresas\n\n" + erro.Message, "ERRO!");
            }
        }

        private void CarregarDadosFoneEmpresa()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idFoneEmpresa,numeroEmpresa,operFoneEmpresa,descFoneEmpresa,nomeEmpresa FROM foneempresa INNER JOIN empresa ON foneempresa.idEmpresa = empresa.idEmpresa WHERE idFoneEmpresa=@codFone";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneEmpresa);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.numeroEmpresa = reader.GetString(1);
                    Variaveis.operFoneEmpresa = reader.GetString(2);
                    Variaveis.descFoneEmpresa = reader.GetString(3);
                    Variaveis.nomeEmpresa = reader.GetString(4);
                    txtCodigo.Text = Variaveis.numeroEmpresa.ToString();
                    txtCodigo.Text = Variaveis.operFoneEmpresa;
                    txtCodigo.Text = Variaveis.descFoneEmpresa;
                    txtCodigo.Text = Variaveis.nomeEmpresa;
                }
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados de telefone da empresa!\n\n" + ex.Message, "ERRO");
            }
        }

        private void frmTelEmpresa_Load(object sender, EventArgs e)
        {
            pnlTelEmpresa.Location = new Point(this.Width / 2 - pnlTelEmpresa.Width / 2, this.Height / 2 - pnlTelEmpresa.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            CarregarEmpresas();
            cmbEmpresa.Text = Variaveis.nomeEmpresa;

            if (Variaveis.funcao == "ALTERAR FONE")
            {
                CarregarDadosFoneEmpresa();
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
            lblNomeEmpresa.ForeColor = Color.FromArgb(70, 10, 45);
            lblNumeroTelefone.ForeColor = Color.FromArgb(70, 10, 45);
            lblOperadora.ForeColor = Color.FromArgb(70, 10, 45);
            lblDescrição.ForeColor = Color.FromArgb(70, 10, 45);


            if (cmbEmpresa.Text == String.Empty)
            {
                lblNomeEmpresa.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a empresa");
                cmbEmpresa.Focus();
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
                Variaveis.nomeEmpresa = cmbEmpresa.Text;
                Variaveis.numeroEmpresa = mkdTel.Text;
                Variaveis.operFoneEmpresa = cmbOperadora.Text;
                Variaveis.descFoneEmpresa = txtDescricao.Text;

                if (Variaveis.funcao == "CADASTRAR FONE")
                {
                    InserirFoneEmpresa();
                }
                else if (Variaveis.funcao == "ALTERAR FONE")
                {
                    AlterarFoneEmpresa();
                }

                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            cmbEmpresa.SelectedIndex = -1;
            mkdTel.Clear();
            cmbOperadora.SelectedIndex = -1;
            txtDescricao.Clear();

            cmbEmpresa.Focus();
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            cmbEmpresa.SelectedIndex = -1;
            mkdTel.Clear();
            cmbOperadora.SelectedIndex = -1;
            txtDescricao.Clear();

            cmbEmpresa.Focus();
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

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja sair?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
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
    }
}
