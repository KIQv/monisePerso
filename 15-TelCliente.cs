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
    public partial class frmTelCliente : Form
    {
        public frmTelCliente()
        {
            InitializeComponent();
        }

        private void InserirFoneCliente()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO fonecliente(idFoneCliente,numeroFoneCliente,operFoneCliente,descFoneCliente,idCliente)VALUES(DEFAULT,@numero,@operadora,@descricao,@codEmpresa)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroCliente);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneCliente);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneCliente);
                cmd.Parameters.AddWithValue("@codEmpresa", Variaveis.codCliente);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone da empresa cadastrada com sucesso!", "CADASTRO DO TELEFONE DA EMPRESA");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone do cliente!\n\n" + ex.Message, "Erro.");
            }
        }

        private void AlterarFoneCliente() //ALTERAR EM telEmpresa
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE fonecliente SET idFoneCliente=@codFone,numeroFoneCliente=@numero,operFoneCliente=@operadora,descFoneCliente=@descricao";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@numero", Variaveis.numeroCliente);
                cmd.Parameters.AddWithValue("@operadora", Variaveis.operFoneCliente);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descFoneCliente);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneCliente);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Telefone da empresa alterada com sucesso!", "ALTERAÇÃO DO TELEFONE DO CLIENTE");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar telefone do  cliente!\n\n" + ex.Message, "Erro.");
            }
        }

        private void CarregarClientes() //CARREGAR EM telClientes
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idCliente,nomeCliente FROM cliente ORDER BY nomeCliente";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cmbClientes.DataSource = dt;
                cmbClientes.DisplayMember = "nomeCliente";
                cmbClientes.ValueMember = "idCliente";
                cmbClientes.SelectedIndex = -1;
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar a lista de clientes\n\n" + erro.Message, "ERRO!");
            }
        }

        private void CarregarDadosFoneCliente()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idFoneCliente,numeroFoneCliente,operFoneCliente,descFoneCliente,nomeCliente FROM fonecliente INNER JOIN cliente ON fonecliente.idCliente = cliente.idCliente WHERE idFoneCliente=@codFone";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneCliente);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.numeroCliente = reader.GetString(1);
                    Variaveis.operFoneCliente = reader.GetString(2);
                    Variaveis.descFoneCliente = reader.GetString(3);
                    Variaveis.nomeCliente = reader.GetString(4);
                    txtCodigo.Text = Variaveis.numeroCliente.ToString();
                    txtCodigo.Text = Variaveis.operFoneCliente;
                    txtCodigo.Text = Variaveis.descFoneCliente;
                    txtCodigo.Text = Variaveis.nomeCliente;
                }
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar os dados de telefone do cliente!\n\n" + ex.Message, "ERRO");
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

        private void frmTelCliente_Load(object sender, EventArgs e)
        {
            pnlTelCliente.Location = new Point(this.Width / 2 - pnlTelCliente.Width / 2, this.Height / 2 - pnlTelCliente.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            CarregarClientes();
            cmbClientes.Text = Variaveis.nomeCliente;

            if (Variaveis.funcao == "ALTERAR FONE")
            {
                CarregarDadosFoneCliente();
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
            lblNomeCliente.ForeColor = Color.FromArgb(70, 10, 45);
            lblNumeroTelefone.ForeColor = Color.FromArgb(70, 10, 45);
            lblOperadora.ForeColor = Color.FromArgb(70, 10, 45);
            lblDescrição.ForeColor = Color.FromArgb(70, 10, 45);


            if (cmbClientes.Text == String.Empty)
            {
                lblNomeCliente.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a empresa");
                cmbClientes.Focus();
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
                Variaveis.nomeCliente = cmbClientes.Text;
                Variaveis.numeroCliente = mkdTel.Text;
                Variaveis.operFoneCliente = cmbOperadora.Text;
                Variaveis.descFoneCliente = txtDescricao.Text;

                if (Variaveis.funcao == "CADASTRAR FONE")
                {
                    InserirFoneCliente();
                }
                else if (Variaveis.funcao == "ALTERAR FONE")
                {
                    AlterarFoneCliente();
                }

                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            cmbClientes.SelectedIndex = -1;
            mkdTel.Clear();
            cmbOperadora.SelectedIndex = -1;
            txtDescricao.Clear();

            cmbClientes.Focus();
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
