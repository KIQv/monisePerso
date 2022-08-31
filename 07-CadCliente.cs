using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monisePerso
{
    public partial class frmCadCliente : Form
    {
        public frmCadCliente()
        {
            InitializeComponent();
        }

        public bool ValidarFtp()
        {
            if (string.IsNullOrEmpty(Variaveis.enderecoServidorFtp) ||
                string.IsNullOrEmpty(Variaveis.usuarioFtp) ||
                string.IsNullOrEmpty(Variaveis.senhaFtp))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // CONVERTER A IMAGEM EM BYTES
        public byte[] GetImgToByte(string caminhoArquivoFtp)
        {
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential(Variaveis.usuarioFtp, Variaveis.senhaFtp);
            byte[] imageToByte = ftpClient.DownloadData(caminhoArquivoFtp);
            return imageToByte;
        }
        // CONVERTER A IMAGEM DE BYTE PARA BITMAP/IMG
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        private void InserirCliente()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO `cliente`(`idCliente`,`nomeCliente`,`emailCliente`,`senhaCliente`,`statusCliente`,`dataCadCliente`,`fotoCliente`) VALUES (DEFAULT,@nome,@email,@senha,@status,@dataCad,@foto)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeCliente);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailCliente);
                cmd.Parameters.AddWithValue("@senha", Variaveis.senhaCliente);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusCliente);
                cmd.Parameters.AddWithValue("@dataCad", Variaveis.dataCadCliente.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@foto", Variaveis.fotoCliente);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente cadastrado com sucesso!", "CADASTRO DO CLIENTE");
                banco.Desconectar();

                // verificar credenciais e fazer upload da imagem
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoCliente))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "cliente/" + Path.GetFileName(Variaveis.fotoCliente);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoCliente, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao cadastrar o cliente!\n\n" + erro.Message, "ERRO!");
            }
        }

        private void AlterarCliente()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE cliente SET nomeCliente=@nome,emailCliente=@email,senhaCliente=@senha,statusCliente=@status WHERE idCliente=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeCliente);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailCliente);
                cmd.Parameters.AddWithValue("@senha", Variaveis.senhaCliente);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusCliente);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codCliente);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Cliente alterado com sucesso!", "Alteração do cliente");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar cliente\n\n" + ex, "ERRO");
            }
        }

        private void AlterarFotoCliente()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE cliente SET fotoCliente=@foto WHERE idCliente=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@foto", Variaveis.fotoCliente);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codCliente);
                cmd.ExecuteNonQuery();
                banco.Desconectar();
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoCliente))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "cliente/" + Path.GetFileName(Variaveis.fotoCliente);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoCliente, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Foto não selecionada\n\n" + ex.Message, "ERRO");
                        }
                    }
                }
                MessageBox.Show("Foto alterada com sucesso!", "Alteração do cliente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar a foto do cliente\n\n" + ex.Message, "ERRO");
            }
        }

        private void CarregarDadosCliente()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM cliente WHERE idCliente=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codCliente);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.nomeCliente = reader.GetString(1);
                    Variaveis.emailCliente = reader.GetString(2);
                    Variaveis.senhaCliente = reader.GetString(3);
                    Variaveis.statusCliente = reader.GetString(4);
                    Variaveis.dataCadCliente = DateTime.Parse(reader.GetString(5));
                    Variaveis.fotoCliente = reader.GetString(6);
                    Variaveis.fotoCliente = Variaveis.fotoCliente.Remove(0, 8);
                    txtCodigo.Text = Variaveis.codCliente.ToString();
                    txtNomeCliente.Text = Variaveis.nomeCliente;
                    txtEmail.Text = Variaveis.emailCliente;
                    txtSenha.Text = Variaveis.senhaCliente;
                    cmbStatus.Text = Variaveis.statusCliente;
                    mkdData.Text = Variaveis.dataCadCliente.ToString("dd/MM/yyyy");
                    pctFoto.Image = ByteToImage(GetImgToByte(Variaveis.enderecoServidorFtp + "cliente/" + Variaveis.fotoCliente));
                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar os dados do cliente! \n\n" + erro, "ERRO");
            }
        }

        private void CarregarClienteCadastrado()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idCliente FROM cliente WHERE nomeCliente=@nome AND emailcliente=@email";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeCliente);
                cmd.Parameters.AddWithValue("@email", Variaveis.emailCliente);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { }
                Variaveis.codCliente = reader.GetInt32(0);
                txtCodigo.Text = Variaveis.codCliente.ToString();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar cliente cadastrado! \n\n" + erro.Message, "ERRO");
            }
        }

        private void CarregarFoneCliente()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `fonecliente` WHERE `idCliente`=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codCliente);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTelCliente.DataSource = dt;
                dgvTelCliente.Columns[0].HeaderText = "CÓDIGO";
                dgvTelCliente.Columns[0].Visible = false;
                dgvTelCliente.Columns[1].HeaderText = "NÚMERO TELEFONE";
                dgvTelCliente.Columns[2].HeaderText = "OPERADORA";
                dgvTelCliente.Columns[3].HeaderText = "DESCRIÇÃO";
                dgvTelCliente.Columns[4].HeaderText = "CLIENTE";
                dgvTelCliente.Columns[4].Visible = false;
                dgvTelCliente.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar o telefone do Cliente. \n\n" + erro.Message);
            }
        }

        private void ExcluirFoneCliente()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM 'Fonecliente` WHERE `idFoneCliente =@codFone";
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codFone", Variaveis.codFoneCliente);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvTelCliente.DataSource = dt;
                dgvTelCliente.ClearSelection();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o Telefone do Cliente. \n\n" + erro.Message);
            }
        }

        private void frmCadCliente_Load(object sender, EventArgs e)
        {
            pnlCadCliente.Location = new Point(this.Width / 2 - pnlCadCliente.Width / 2, this.Height / 2 - pnlCadCliente.Height / 2);

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
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            new frmProdutos().Show();
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

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNomeCliente.ForeColor = Color.FromArgb(70, 10, 45);
            lblEmail.ForeColor = Color.FromArgb(70, 10, 45);
            lblSenha.ForeColor = Color.FromArgb(70, 10, 45);
            lblStatus.ForeColor = Color.FromArgb(70, 10, 45);
            lblFoto.BackColor = Color.FromArgb(70, 10, 45);

            if (txtNomeCliente.Text == String.Empty)
            {
                lblNomeCliente.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o nome do cliente");
                txtNomeCliente.Focus();
            }
            else if (txtEmail.Text == String.Empty)
            {
                lblEmail.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o e-mail do cliente");
                txtEmail.Focus();
            }
            else if (txtSenha.Text == String.Empty)
            {
                lblSenha.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a senha");
                txtSenha.Focus();
            }
            else if (cmbStatus.Text == String.Empty)
            {
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o status");
            }
            else if (Variaveis.atFotoCliente != "S")
            {
                lblFoto.BackColor = Color.Red;
                MessageBox.Show("Favor selecionar uma foto");
            }
            else
            {
                Variaveis.nomeCliente = txtNomeCliente.Text;
                Variaveis.emailCliente = txtEmail.Text;
                Variaveis.senhaCliente = txtSenha.Text;
                Variaveis.statusCliente = cmbStatus.Text;
                mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Variaveis.dataCadCliente = DateTime.Parse(mkdData.Text);
                //Variaveis.fotoCliente = "cliente/" + nomeFoto;

                if (Variaveis.funcao == "CADASTRAR")
                {
                    InserirCliente();
                    CarregarClienteCadastrado();
                }
                else if (Variaveis.funcao == "ALTERAR")
                {
                    AlterarCliente();
                    if (Variaveis.atFotoCliente == "S")
                    {
                        AlterarFotoCliente();
                    }
                }

                pnlTelCliente.Enabled = true;
                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private void pctFoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofdFoto = new OpenFileDialog();
                ofdFoto.Multiselect = false;
                ofdFoto.FileName = "";
                ofdFoto.InitialDirectory = @"C:";
                ofdFoto.Title = "Selecione uma foto";
                ofdFoto.Filter = "JPG ou PNG (*.jpg ou *.png)|*.jpg;*.png";
                ofdFoto.CheckFileExists = true;
                ofdFoto.CheckPathExists = true;
                ofdFoto.RestoreDirectory = true;
                DialogResult result = ofdFoto.ShowDialog();
                pctFoto.Image = Image.FromFile(ofdFoto.FileName);
                Variaveis.fotoCliente = "cliente/" + Path.GetFileName(ofdFoto.FileName);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Variaveis.atFotoCliente = "S";
                        Variaveis.caminhoFotoCliente = ofdFoto.FileName;
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show("Erro de segurança, fale com o administrador do sistema. \n Mensagem:\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Você não tem permissão." + ex.Message);
                    }
                    Variaveis.atFotoCliente = "S";
                    btnSalvar.Focus();
                }
            }
            catch
            {
                btnSalvar.Focus();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            lblNomeCliente.ForeColor = Color.FromArgb(70, 10, 45);
            lblEmail.ForeColor = Color.FromArgb(70, 10, 45);
            lblSenha.ForeColor = Color.FromArgb(70, 10, 45);
            lblStatus.ForeColor = Color.FromArgb(70, 10, 45);
            lblFoto.BackColor = Color.FromArgb(70, 10, 45);

            txtNomeCliente.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
            cmbStatus.SelectedIndex = -1;
            mkdData.Clear();
            pctFoto.Image = Properties.Resources.semimagem;

            txtNomeCliente.Focus();
        }
    }
}
