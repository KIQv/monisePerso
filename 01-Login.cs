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

            if(Variaveis.usuario == "kaique" && Variaveis.senha == "123")
            {
                Variaveis.nivel = "ADMINISTRADOR";
                new frmMenu().Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Acesso negado! Digite um usuario valido");
                txtUsuario.Clear();
                txtSenha.Clear();
                txtUsuario.Focus();
            }
        }
    }
}
