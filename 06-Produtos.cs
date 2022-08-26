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

        private void frmProdutos_Load(object sender, EventArgs e)
        {
            pnlProdutos.Location = new Point(this.Width / 2 - pnlProdutos.Width / 2, this.Height / 2 - pnlProdutos.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;
        }
    }
}
