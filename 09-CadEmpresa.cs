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
    public partial class frmCadEmpresa : Form
    {
        public frmCadEmpresa()
        {
            InitializeComponent();
        }

        private void frmCadEmpresa_Load(object sender, EventArgs e)
        {
            pnlCadEmpresa.Location = new Point(this.Width / 2 - pnlCadEmpresa.Width / 2, this.Height / 2 - pnlCadEmpresa.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            Variaveis.linhaFoneSelecionada = -1;

            if (Variaveis.funcao == "ALTERAR")
            {
                pnlTelEmpresa.Enabled = true;
                lblTitulo.Text = "ALTERAÇÃO DA EMPRESA";
                CarregarDadosEmpresa();
                CarregarFoneEmpresa();
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
        private void InserirEmpresa()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO `empresa`(`idEmpresa`, `nomeEmpresa`, `cnpjCpfEmpresa`, `razaoSocialEmpresa`, `emailEmpresa`, `statusEmpresa`, `dataCadEmpresa`, `horarioAtendEmpresa`) VALUES (DEFAULT,@nome,@cnpjCpf,@razaoSocialEmpresa,@email,@status,@dataCad,@horario)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao); cmd.Parameters.AddWithValue("@none", Variaveis.nomeEmpresa);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeEmpresa);
                cmd.Parameters.AddWithValue("@cnpjCpf", Variaveis.cnpjCpf);
                cmd.Parameters.AddWithValue("@razaoSocialEmpresa", Variaveis.razaoSocial);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailEmpresa);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusEmpresa);
                cmd.Parameters.AddWithValue("@dataCad", Variaveis.dataCadEmpresa.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@horario", Variaveis.horarioAtendEmpresa.ToString("HH:mm"));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Empresa cadastrada com sucesso!", "CADASTRO DA EMPRESA");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar a Empresa!\n\n" + erro.Message, "ERRO!");
            }
        }

        private void AlterarEmpresa()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE `empresa` SET `nomeEmpresa`=@nome, `cnpjCpfEmpresa`=@cnpjCpf,`razaoSocialEmpresa`=@razaoSocialEmpresa,`emailEmpresa`=@email,`statusEmpresa`=@status,`horarioAtendEmpresa`=@horario WHERE `idEmpresa`=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeEmpresa);
                cmd.Parameters.AddWithValue("@cnpjCpf", Variaveis.cnpjCpf);
                cmd.Parameters.AddWithValue("@razaoSocialEmpresa", Variaveis.razaoSocial);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailEmpresa);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusEmpresa);
                cmd.Parameters.AddWithValue("@horario", Variaveis.horarioAtendEmpresa.ToString("HH:mm"));
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codEmpresa);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Empresa alterada com sucesso!", "ALTERAÇÃO DA EMPRESA");
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao alterar a empresa!\n\n" + erro.Message, "ERRO!");
            }
        }

        private void CarregarDadosEmpresa()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM empresa WHERE idEmpresa=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codEmpresa);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.nomeEmpresa = reader.GetString(1);
                    Variaveis.cnpjCpf = reader.GetString(2);
                    Variaveis.razaoSocial = reader.GetString(3);
                    Variaveis.emailEmpresa = reader.GetString(4);
                    Variaveis.statusEmpresa = reader.GetString(5);
                    Variaveis.dataCadEmpresa = reader.GetDateTime(6);
                    Variaveis.horarioAtendEmpresa = DateTime.Parse(reader.GetString(7));

                    txtCodigo.Text = Variaveis.codEmpresa.ToString();
                    txtNomeEmpresa.Text = Variaveis.nomeEmpresa;
                    mkdCnpjCpf.Text = Variaveis.cnpjCpf;
                    if (mkdCnpjCpf.Text.Length > 15)
                    {
                        radCnpj.Checked = true;
                    }
                    else
                    {
                        radCpf.Checked = true;
                    }
                    txtRazaoSocial.Text = Variaveis.razaoSocial;
                    txtEmailEmpresa.Text = Variaveis.emailEmpresa;
                    cmbStatus.Text = Variaveis.statusEmpresa;
                    mkdData.Text = Variaveis.dataCadEmpresa.ToString("dd/MM/yyyy");
                    cmbHorarioAtendimento.Text = Variaveis.horarioAtendEmpresa.ToString("HH:mm");
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar os dados da empresa!\n\n" + erro, "ERRO!");
            }
        }

        private void CarregarEmpresaCadastrada()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idEmpresa FROM empresa WHERE nomeEmpresa=@nome AND cnpjCpfEmpresa=@cnpjCpf";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeEmpresa);
                cmd.Parameters.AddWithValue("@cnpjCpf", Variaveis.cnpjCpf);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.codEmpresa = reader.GetInt32(0);
                    txtCodigo.Text = Variaveis.codEmpresa.ToString();
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar empresa cadastrada!\n\n" + erro.Message, "ERRO");
            }
        }

        private void CarregarFoneEmpresa()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `foneempresa` WHERE idEmpresa=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codEmpresa);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvTelEmpresa.DataSource = dt;
                dgvTelEmpresa.Columns[0].HeaderText = "CÓDIGO";
                dgvTelEmpresa.Columns[0].Visible = false;
                dgvTelEmpresa.Columns[1].HeaderText = "NÚMERO TELEFONE";
                dgvTelEmpresa.Columns[2].HeaderText = "OPERADORA";
                dgvTelEmpresa.Columns[3].HeaderText = "DESCRIÇÃO";
                dgvTelEmpresa.Columns[4].HeaderText = "EMPRESA";
                dgvTelEmpresa.Columns[4].Visible = false;
                dgvTelEmpresa.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar o telefone da Empresa.\n\n" + erro.Message);
            }
        }

        private void ExcluirFoneEmpresa()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM `foneempresa` WHERE `idFoneEmpresa`=@codFone";
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneEmpresa);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
        
                da.Fill(dt);

                dgvTelEmpresa.DataSource = dt;

                dgvTelEmpresa.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o Telefone da Empresa\n\n" + erro.Message);
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
            new frmAplicativo().Show();
            Close();
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {
            new frmEmail().Show();
            Close();
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNomeEmpresa.ForeColor = Color.FromArgb(70, 10, 45);
            radCnpj.ForeColor = Color.FromArgb(70, 10, 45);
            radCpf.ForeColor = Color.FromArgb(70, 10, 45);
            lblRazaoSocial.ForeColor = Color.FromArgb(70, 10, 45);
            lblEmailEmpresa.ForeColor = Color.FromArgb(70, 10, 45);
            lblStatus.ForeColor = Color.FromArgb(70, 10, 45);
            lblHorarioAtendimento.ForeColor = Color.FromArgb(70, 10, 45);

            if (txtNomeEmpresa.Text == String.Empty)
            {
                lblNomeEmpresa.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o nome da empresa");
                txtNomeEmpresa.Focus();
            }
            else if (mkdCnpjCpf.MaskCompleted == false)
            {
                radCnpj.ForeColor = Color.Red;
                radCpf.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o CNPJ ou CPF");
            }
            else if (txtRazaoSocial.Text == String.Empty)
            {
                lblRazaoSocial.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a razão social");
                txtRazaoSocial.Focus();

            }
            else if (txtEmailEmpresa.Text == String.Empty)
            {
                lblEmail.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o e-mail");
                txtEmailEmpresa.Focus();
            }
            else if (cmbStatus.Text == String.Empty)
            {
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o status");
                cmbStatus.Focus();
            }
            else if (cmbHorarioAtendimento.Text == String.Empty)
            {
                lblHorarioAtendimento.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o Horário de Atendimento");
                cmbHorarioAtendimento.Focus();
            }
            else
            {
                Variaveis.nomeEmpresa = txtNomeEmpresa.Text;
                Variaveis.cnpjCpf = mkdCnpjCpf.Text;
                Variaveis.razaoSocial = txtRazaoSocial.Text;
                Variaveis.emailEmpresa = txtEmailEmpresa.Text;
                Variaveis.statusEmpresa = cmbStatus.Text;
                if (Variaveis.funcao == "CADASTRAR")
                {
                    mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    Variaveis.dataCadEmpresa = DateTime.Parse(mkdData.Text);
                }
                Variaveis.horarioAtendEmpresa = DateTime.Parse(cmbHorarioAtendimento.Text);

                if (Variaveis.funcao == "CADASTRAR")
                {
                    InserirEmpresa();
                    CarregarEmpresaCadastrada();

                }
                else if (Variaveis.funcao == "ALTERAR")
                {
                    AlterarEmpresa();

                }

                pnlTelEmpresa.Enabled = true;
                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            lblNomeEmpresa.ForeColor = Color.FromArgb(70, 10, 45);
            radCnpj.ForeColor = Color.FromArgb(70, 10, 45);
            radCpf.ForeColor = Color.FromArgb(70, 10, 45);
            lblRazaoSocial.ForeColor = Color.FromArgb(70, 10, 45);
            lblEmailEmpresa.ForeColor = Color.FromArgb(70, 10, 45);
            lblStatus.ForeColor = Color.FromArgb(70, 10, 45);
            lblHorarioAtendimento.ForeColor = Color.FromArgb(70, 10, 45);

            txtNomeEmpresa.Clear();
            mkdCnpjCpf.Clear();
            txtRazaoSocial.Clear();
            txtEmailEmpresa.Clear();
            cmbStatus.SelectedIndex = -1;
            mkdData.Clear();
            cmbHorarioAtendimento.SelectedIndex = -1;

            txtNomeEmpresa.Focus();
        }

        private void radCnpj_CheckedChanged(object sender, EventArgs e)
        {
            mkdCnpjCpf.Mask = "00,000,000/0000-00";
            mkdCnpjCpf.Focus();
        }

        private void radCpf_CheckedChanged(object sender, EventArgs e)
        {
            mkdCnpjCpf.Mask = "000,000,000-00";
            mkdCnpjCpf.Focus();
        }

        private void txtNomeEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtRazaoSocial.Focus();
            }
        }

        private void txtRazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtEmailEmpresa.Focus();
            }
        }

        private void txtEmailEmpresa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                radCnpj.Focus();
            }
        }

        private void mkdCnpjCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if ((mkdCnpjCpf.MaskCompleted == true) && (mkdCnpjCpf.MaskCompleted == true))
                {
                   cmbStatus.Focus();
                }
                else
                {
                    MessageBox.Show("Complete o CNPJ ou CPF");
                    mkdCnpjCpf.Focus();
                }
            }
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                cmbHorarioAtendimento.Focus();
            }
        }

        private void cmbHorarioAtendimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                if (Variaveis.funcao == "CADASTRAR")
                {
                    mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                btnSalvar.Focus();
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            Variaveis.funcao = "CADASTRAR FONE";
            new frmTelEmpresa().Show();
            Hide();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (Variaveis.linhaFoneSelecionada >= 0)
            {
                Variaveis.funcao = "ALTERAR FONE";
                new frmTelEmpresa().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Para alterar selecione uma linha.");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Variaveis.linhaFoneSelecionada >= 0)
            {
                var resultado = MessageBox.Show("Deseja realmente excluir?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ExcluirFoneEmpresa();
                }
            }
            else
            {
                MessageBox.Show("Para excluir selecione uma linha.");
            }
        }
    }
}
