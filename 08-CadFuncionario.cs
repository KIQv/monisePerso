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
    public partial class frmCadFuncionario : Form
    {
        public frmCadFuncionario()
        {
            InitializeComponent();
        }

        private void InserirFuncionario()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO `funcionario`(`idFuncionario`, `nomeFuncionario`, `emailFuncionario`, `senhaFuncionario`, `nivelFuncionario`, `statusFuncionario`, `dataCadFuncionario`, `horarioTrabalhoFuncionario`, `idEmpresa`) VALUES (DEFAULT,@nome,@email,@senha,@nivel,@status,@dataCad,horario,@codEmpresa)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeFuncionario);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailFuncionario);
                cmd.Parameters.AddWithValue("@senha", Variaveis.senhaFuncionario);
                cmd.Parameters.AddWithValue("@nivel", Variaveis.nivelFuncionario);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusFuncionario);
                cmd.Parameters.AddWithValue("@dataCad", Variaveis.dataCadFuncionario.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@horario", Variaveis.horarioFuncionario.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@codEmpresa", Variaveis.codEmpresa);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Funcionario cadastrado com sucesso!", "CADASTRO DO CLIENTE");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar o Funcionario!\n\n" + erro.Message, "ERRO!");
            }
        }

        private void AlterarFuncionario()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE funcionario SET nomeFuncionario=@nome,emailFuncionario=@email,senhaFuncionario=@senha,nivelFuncionario=@nivel,statusFuncionario=@status,horarioTrabalhoFuncionario=@horario,idEmpresa=@codEmpresa, WHERE idFuncionario=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeFuncionario);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailFuncionario);
                cmd.Parameters.AddWithValue("@senha", Variaveis.senhaFuncionario);
                cmd.Parameters.AddWithValue("@nivel", Variaveis.nivelFuncionario);
                cmd.Parameters.AddWithValue("@horario", Variaveis.horarioFuncionario);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusFuncionario);
                cmd.Parameters.AddWithValue("@codEmpresa", Variaveis.codEmpresa);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codFuncionario);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Funcionario alterado com sucesso!", "Alteração do Funcionario");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar Funcionario\n\n" + ex, "ERRO");
            }
        }

        private void CarregarDadosFuncionario()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM funcionario WHERE idFuncionario=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codFuncionario);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.nomeFuncionario = reader.GetString(1);
                    Variaveis.emailFuncionario = reader.GetString(2);
                    Variaveis.senhaFuncionario = reader.GetString(3);
                    Variaveis.nivelFuncionario = reader.GetString(4);
                    Variaveis.statusFuncionario = reader.GetString(5);
                    Variaveis.dataCadFuncionario = DateTime.Parse(reader.GetString(6));
                    Variaveis.horarioFuncionario = DateTime.Parse(reader.GetString(7));
                    Variaveis.codEmpresa = reader.GetInt32(8);

                    txtCodigo.Text = Variaveis.codFuncionario.ToString();
                    txtNomeFuncionario.Text = Variaveis.nomeFuncionario;
                    txtEmailFuncionario.Text = Variaveis.emailFuncionario;
                    txtSenha.Text = Variaveis.senhaFuncionario;
                    cmbNivel.Text = Variaveis.nivelFuncionario;
                    cmbStatus.Text = Variaveis.statusFuncionario;
                    mkdData.Text = Variaveis.dataCadFuncionario.ToString("dd/MM/yyyy");
                    mkdData.Text = Variaveis.dataCadFuncionario.ToString("HH: mm");
                    cmbEmpresaFuncionario.Text = Variaveis.codEmpresa.ToString();
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar os dados do funcionario! \n\n" + erro, "ERRO");
            }
        }

        private void CarregarFuncionarioCadastrado()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idFuncionario FROM funcionario WHERE nomeFuncionario=@nome AND emailFuncionario=@email";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeFuncionario);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailFuncionario);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { }
                Variaveis.codFuncionario = reader.GetInt32(0);
                txtCodigo.Text = Variaveis.codFuncionario.ToString();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar funcionario cadastrado! \n\n" + erro.Message, "ERRO");
            }
        }

        private void CarregarFoneFuncionario()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `fonefuncionario` WHERE `idFuncionario`=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codFuncionario);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTelFuncionario.DataSource = dt;
                dgvTelFuncionario.Columns[0].HeaderText = "CÓDIGO";
                dgvTelFuncionario.Columns[0].Visible = false;
                dgvTelFuncionario.Columns[1].HeaderText = "NÚMERO TELEFONE";
                dgvTelFuncionario.Columns[2].HeaderText = "OPERADORA";
                dgvTelFuncionario.Columns[3].HeaderText = "DESCRIÇÃO";
                dgvTelFuncionario.Columns[4].HeaderText = "FUNCIONARIO";
                dgvTelFuncionario.Columns[4].Visible = false;
                dgvTelFuncionario.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar o telefone do Funcionario. \n\n" + erro.Message);
            }
        }

        private void ExcluirFoneFuncionario()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM 'fonefuncionario` WHERE `idFoneFuncionario =@codFone";
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneFuncionario);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTelFuncionario.DataSource = dt;
                dgvTelFuncionario.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o Telefone do Funcionario. \n\n" + erro.Message);
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

                cmbEmpresaFuncionario.DataSource = dt;
                cmbEmpresaFuncionario.DisplayMember = "nomeEmpresa";
                cmbEmpresaFuncionario.ValueMember = "idEmpresa";
                cmbEmpresaFuncionario.SelectedIndex = -1;
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar a lista de empresas\n\n" + erro.Message, "ERRO!");
            }
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
            //new frmAplicativo().Show();
            //Close();
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            //new frmEmail().Show();
            //Close();
        }

        private void frmCadFuncionario_Load(object sender, EventArgs e)
        {
            pnlCadFuncionario.Location = new Point(this.Width / 2 - pnlCadFuncionario.Width / 2, this.Height / 2 - pnlCadFuncionario.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

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

            Variaveis.linhaFoneSelecionada = -1;

            if (Variaveis.funcao == "ALTERAR")
            {
                lblTitulo.Text = "ALTERAÇÃO DO FUNCIONARIO";
                pnlTelFuncionario.Enabled = true;
                CarregarDadosFuncionario();
                CarregarFoneFuncionario();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNomeFuncionario.ForeColor = Color.FromArgb(73, 73, 73);
            lblEmail.ForeColor = Color.FromArgb(73, 73, 73);
            lblSenha.ForeColor = Color.FromArgb(73, 73, 73);
            lblNivel.ForeColor = Color.FromArgb(73, 73, 73);
            lblStatus.ForeColor = Color.FromArgb(73, 73, 73);
            lblHorarioTrabalho.ForeColor = Color.FromArgb(73, 73, 73);
            lblEmpresaFuncionario.ForeColor = Color.FromArgb(73, 73, 73);

            if (txtNomeFuncionario.Text == String.Empty)
            {
                lblNomeFuncionario.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o nome do Funcionario");
                txtNomeFuncionario.Focus();
            }
            else if (txtEmailFuncionario.Text == String.Empty)
            {
                lblEmailFuncionario.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o e-mail do Funcionario");
                txtEmailFuncionario.Focus();
            }
            else if (txtSenha.Text == String.Empty)
            {
                lblSenha.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a senha");
                txtSenha.Focus();
            }
            else if (cmbNivel.Text == String.Empty)
            {
                lblNivel.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o Nivel");
            }
            else if (cmbStatus.Text == String.Empty)
            {
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o status");
            }
            else if (cmbHorarioTrabalho.Text == String.Empty)
            {
                lblHorarioTrabalho.BackColor = Color.Red;
                MessageBox.Show("Favor selecionar um horario");
            }
            else if (cmbEmpresaFuncionario.Text == String.Empty)
            {
                lblEmpresaFuncionario.BackColor = Color.Red;
                MessageBox.Show("Favor selecionar uma empresa");
            }
            else
            {
                Variaveis.nomeFuncionario = txtNomeFuncionario.Text;
                Variaveis.emailFuncionario = txtEmailFuncionario.Text;
                Variaveis.senhaFuncionario = txtSenha.Text;
                Variaveis.nivelFuncionario = cmbNivel.Text;
                Variaveis.statusFuncionario = cmbStatus.Text;
                Variaveis.nomeEmpresa = cmbEmpresaFuncionario.Text;

                if (Variaveis.funcao == "CADASTRAR")
                {
                    mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    Variaveis.dataCadFuncionario = DateTime.Parse(mkdData.Text);
                }
                Variaveis.horarioFuncionario = DateTime.Parse(cmbHorarioTrabalho.Text);

                if (Variaveis.funcao == "CADASTRAR")
                {
                    InserirFuncionario();
                    CarregarFuncionarioCadastrado();

                }
                else if (Variaveis.funcao == "ALTERAR")
                {
                    AlterarFuncionario();

                }

                pnlTelFuncionario.Enabled = true;
                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }
    }
}

