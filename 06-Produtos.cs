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
    public partial class frmProdutos : Form
    {
        public frmProdutos()
        {
            InitializeComponent();
        }

        private void CarregarProduto()
        {
            try
            { 
                banco.Conectar();
                string selecionar = "SELECT * FROM `produtocompleto`"; 
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;

                dgvProdutos.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o produto. \n\n" + erro.Message);
            }
        }

        private void CarregarProdutoAtivo()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `produtoativo`"; 
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;

                dgvProdutos.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o produto. \n\n" + erro.Message);
            }
        }

        private void CarregarProdutoInativo() 
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `produtoinativo`"; 
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;

                dgvProdutos.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o produto. \n\n" + erro.Message);
            }
        }

        private void CarregarProdutoNome()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `produtocompleto` WHERE `NOME DO PRODUTO` LIKE '%" + Variaveis.nomeProduto + "%'";   
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;

                dgvProdutos.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o produto. \n\n" + erro.Message);
            }
        }

        private void ExcluirProduto()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM `produto` WHERE `idProduto`=@codigo"; 
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;

                dgvProdutos.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o produto. \n\n" + erro.Message);
            }
        }

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            pnlProdutos.Location = new Point(this.Width / 2 - pnlProdutos.Width / 2, this.Height / 2 - pnlProdutos.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            CarregarProduto();
        }

        private void chkAtivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAtivo.Checked == true)
            {
                CarregarProdutoAtivo();
                chkInativo.Checked = false;
                chkInativo.Enabled = false;
            }
            else
            {
                CarregarProduto();
                chkInativo.Enabled = true;
            }
        }

        private void chkInativo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInativo.Checked == true)
            {
                CarregarProdutoInativo();
                chkAtivo.Checked = false;
                chkAtivo.Enabled = false;
            }
            else
            {
                CarregarProduto();
                chkAtivo.Enabled = true;
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            if (txtNome.Text == "")
            {
                chkAtivo.Enabled = true;
                CarregarProduto();
            }
            else
            {
                chkAtivo.Checked = false;
                chkAtivo.Enabled = false;
                chkInativo.Checked = false;
                chkInativo.Enabled = false;
                Variaveis.nomeProduto = txtNome.Text; 
                CarregarProdutoNome();
            }
        }

        private void lblProdutos_Click(object sender, EventArgs e)
        {
            new frmProdutos().Show();
            Hide();
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

        private void lblEmail_Click(object sender, EventArgs e)
        {
            //new frmEmail().Show();
            //Hide();
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
    }
}
