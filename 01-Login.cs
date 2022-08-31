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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            pnlLogin.Location = new Point(this.Width / 2 - pnlLogin.Width / 2, this.Height / 2 - pnlLogin.Height / 2);
        }

        private void pctFechar_Click(object sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Deseja realmente fechar?", "Fechar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                txtUsuario.Clear();
                txtSenha.Clear();
                txtUsuario.Focus();
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Variaveis.usuario = txtUsuario.Text;
            Variaveis.senha = txtSenha.Text;

            if (Variaveis.usuario == "Kaique" && Variaveis.senha == "123")
            {
                Variaveis.nivel = "ADMINISTRADOR";
                new frmMenu().Show();
                Hide();
            }
            else
            {
                try
                {
                    banco.Conectar();
                    string selecionar = "SELECT `nomeFuncionario`,`emailFuncionario`,`senhaFuncionario`,`nivelFuncionario` FROM `funcionario` WHERE `emailFuncionario`=@email AND `senhaFuncionario`=@senha AND `statusFuncionario`=@status";
                    MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                    cmd.Parameters.AddWithValue("@email", Variaveis.usuario);
                    cmd.Parameters.AddWithValue("@senha", Variaveis.senha);
                    cmd.Parameters.AddWithValue("@status", "ATIVO");
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Variaveis.usuario = reader.GetString(0);
                        Variaveis.nivel = reader.GetString(3);
                        new frmMenu().Show();
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("ACESSO NEGADO");
                        txtUsuario.Clear();
                        txtSenha.Clear();
                        txtUsuario.Focus();
                    }
                }
                catch
                {
                    MessageBox.Show("ERRO AO ACESSAR O BANCO DE DADOS");
                }
            }
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
