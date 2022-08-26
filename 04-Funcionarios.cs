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
    public partial class frmFuncionarios : Form
    {
        public frmFuncionarios()
        {
            InitializeComponent();
        }

        private void CarregarFuncionario()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `funcionariocompleto`";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFuncionarios.DataSource = dt;

                dgvFuncionarios.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o funcionario. \n\n" + erro.Message);
            }
        }

        private void CarregarFuncionarioAtivo()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `funcionarioativo`";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFuncionarios.DataSource = dt;

                dgvFuncionarios.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o funcionario. \n\n" + erro.Message);
            }
        }

        private void CarregarFuncionarioInativo()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `funcionarioinativo`";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFuncionarios.DataSource = dt;

                dgvFuncionarios.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o funcionario. \n\n" + erro.Message);
            }
        }

        private void CarregarFuncionarioNome()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `funcionariocompleto` WHERE `NOME DO FUNCIONÁRIO` LIKE '%" + Variaveis.nomeFuncionario + "%'";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFuncionarios.DataSource = dt;

                dgvFuncionarios.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o funcionario. \n\n" + erro.Message);
            }
        }

        private void ExcluirFuncionario()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM `funcionario` WHERE `idFuncionario`=@codigo";
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codFuncionario);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvFuncionarios.DataSource = dt;

                dgvFuncionarios.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o funcionario. \n\n" + erro.Message);
            }
        }

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            pnlFuncionarios.Location = new Point(this.Width / 2 - pnlFuncionarios.Width / 2, this.Height / 2 - pnlFuncionarios.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            Variaveis.linhaSelecionada = -1;
            CarregarFuncionario();
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

        private void lblMenu_Click(object sender, EventArgs e)
        {
            new frmMenu().Show();
            Hide();
        }

        private void lblEncomendas_Click(object sender, EventArgs e)
        {
            //new frmEncomendas().Show();
            //Hide();
        }

        private void lblClientes_Click(object sender, EventArgs e)
        {
            new frmClientes().Show();
            Hide();
        }

        private void lblFuncionarios_Click(object sender, EventArgs e)
        {
            new frmFuncionarios().Show();
            Hide();
        }

        private void lblEmpresa_Click(object sender, EventArgs e)
        {
            new frmEmpresa().Show();
            Hide();
        }

        private void lblProdutos_Click(object sender, EventArgs e)
        {
            new frmProdutos().Show();
            Hide();
        }

        private void lblSobre_Click(object sender, EventArgs e)
        {
            //new frmSobre().Show();
            //Hide();
        }

        private void lblAplicativo_Click(object sender, EventArgs e)
        {
            //new frmAplicativo().Show();
            //Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //new frmEmail().Show();
            //Hide();
        }

        private void chkAtivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAtivo.Checked == true)
            {
                CarregarFuncionarioAtivo();
                chkInativo.Checked = false;
                chkInativo.Enabled = false;
            }
            else
            {
                CarregarFuncionario();
                chkInativo.Enabled = true;
            }
        }

        private void chkInativo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInativo.Checked == true)
            {
                CarregarFuncionarioInativo();
                chkAtivo.Checked = false;
                chkAtivo.Enabled = false;
            }
            else
            {
                CarregarFuncionario();
                chkAtivo.Enabled = true;
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                chkAtivo.Enabled = true;
                CarregarFuncionario();
            }
            else
            {
                chkAtivo.Checked = false;
                chkAtivo.Enabled = false;
                chkInativo.Checked = false;
                chkInativo.Enabled = false;
                Variaveis.nomeFuncionario = txtNome.Text;
                CarregarFuncionarioNome();
            }
        }
    }
}
