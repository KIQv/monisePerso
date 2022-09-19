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
    public partial class frmCadProduto : Form
    {
        public frmCadProduto()
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

        private void InserirProduto()
        {
            try
            {
                banco.Conectar();
                string inserir = "INSERT INTO `produto`(`idProduto`, `nomeProduto`, `descProduto`, `tipoProduto`, `valorProduto`, `dataCadProduto`, `fotoProduto1`, `fotoProduto2`, `fotoProduto3`, `fotoProduto4`, `destaqueProduto`, `statusProduto`) VALUES (DEFAULT,@nome,@descricao,@tipo,@valor,@dataCad,@foto1,@foto2,@foto3,@foto4,@destaque,@status)";
                MySqlCommand cmd = new MySqlCommand(inserir, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeProduto);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descProduto);
                cmd.Parameters.AddWithValue("@tipo", Variaveis.tipoProduto);
                cmd.Parameters.AddWithValue("@valor", Variaveis.valorProduto);
                cmd.Parameters.AddWithValue("@dataCad", Variaveis.dataCadProduto.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@foto1", Variaveis.fotoProduto1);
                cmd.Parameters.AddWithValue("@foto2", Variaveis.fotoProduto2);
                cmd.Parameters.AddWithValue("@foto3", Variaveis.fotoProduto3);
                cmd.Parameters.AddWithValue("@foto4", Variaveis.fotoProduto4);
                cmd.Parameters.AddWithValue("@destaque", Variaveis.destaqueProduto);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusProduto);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produto cadastrado com sucesso!", "CADASTRO DO PRODUTO");
                banco.Desconectar();

                // verificar credenciais e fazer upload da imagem
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto1))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto1);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto1, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto2))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto2);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto2, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto3))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto3);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto3, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto4))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto4);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto4, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
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
                MessageBox.Show("Erro ao cadastrar o produto!\n\n" + erro, "ERRO!");
            }
        }

        private void AlterarProduto()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE `produto` SET `nomeProduto`=@nome,`descProduto`=@descricao,`tipoProduto`=@tipo,`valorProduto`=@valor,`destaqueProduto`=@destaque,`statusProduto`=@status WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeProduto);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descProduto);
                cmd.Parameters.AddWithValue("@tipo", Variaveis.tipoProduto);
                cmd.Parameters.AddWithValue("@valor", Variaveis.valorProduto);
                cmd.Parameters.AddWithValue("@destaque", Variaveis.destaqueProduto);
                cmd.Parameters.AddWithValue("@status", Variaveis.statusProduto);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Produto alterado com sucesso!", "Alteração do produto");
                banco.Desconectar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar produto\n\n" + ex, "ERRO");
            }
        }

        private void AlterarFotoProduto1()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE produto SET fotoProduto1=@foto1 WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@foto1", Variaveis.fotoProduto1);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                cmd.ExecuteNonQuery();
                banco.Desconectar();
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto1))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto1);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto1, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Foto alterada com sucesso!", "Alteração do produto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar a foto do produto\n\n" + ex.Message, "ERRO");
            }
        }

        private void AlterarFotoProduto2()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE produto SET fotoProduto2=@foto2 WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@foto2", Variaveis.fotoProduto2);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                cmd.ExecuteNonQuery();
                banco.Desconectar();
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto2))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto2);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto2, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Foto alterada com sucesso!", "Alteração do produto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar a foto do produto\n\n" + ex.Message, "ERRO");
            }
        }
        private void AlterarFotoProduto3()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE produto SET fotoProduto3=@foto3 WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@foto3", Variaveis.fotoProduto3);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                cmd.ExecuteNonQuery();
                banco.Desconectar();
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto3))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto3);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto3, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Foto alterada com sucesso!", "Alteração do produto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar a foto do produto\n\n" + ex.Message, "ERRO");
            }
        }

        private void AlterarFotoProduto4()
        {
            try
            {
                banco.Conectar();
                string alterar = "UPDATE produto SET fotoProduto4=@foto4 WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(alterar, banco.conexao);
                cmd.Parameters.AddWithValue("@foto4", Variaveis.fotoProduto4);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                cmd.ExecuteNonQuery();
                banco.Desconectar();
                if (ValidarFtp())
                {
                    if (!string.IsNullOrEmpty(Variaveis.fotoProduto4))
                    {
                        string urlEnviarArquivo = Variaveis.enderecoServidorFtp + "produto/" + Path.GetFileName(Variaveis.fotoProduto4);
                        try
                        {
                            ftp.EnviarArquivoFtp(Variaveis.caminhoFotoProduto4, urlEnviarArquivo, Variaveis.usuarioFtp, Variaveis.senhaFtp);
                        }
                        catch
                        {
                            MessageBox.Show("Foto não selecionada", "Foto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("Foto alterada com sucesso!", "Alteração do produto");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao alterar a foto do produto\n\n" + ex.Message, "ERRO");
            }
        }

        private void CarregarDadosProduto()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM produto WHERE idProduto=@codigo";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codProduto);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Variaveis.nomeProduto = reader.GetString(1);
                    Variaveis.descProduto = reader.GetString(2);
                    Variaveis.tipoProduto = reader.GetString(3);
                    Variaveis.valorProduto = reader.GetInt32(4);
                    Variaveis.dataCadProduto = DateTime.Parse(reader.GetString(5));
                    Variaveis.destaqueProduto = reader.GetString(10);
                    Variaveis.statusProduto = reader.GetString(11);
                    Variaveis.fotoProduto1 = reader.GetString(6);
                    Variaveis.fotoProduto1 = Variaveis.fotoProduto1.Remove(0, 8);
                    Variaveis.fotoProduto2 = reader.GetString(7);
                    Variaveis.fotoProduto2 = Variaveis.fotoProduto2.Remove(0, 8);
                    Variaveis.fotoProduto3 = reader.GetString(8);
                    Variaveis.fotoProduto3 = Variaveis.fotoProduto3.Remove(0, 8);
                    Variaveis.fotoProduto4 = reader.GetString(9);
                    Variaveis.fotoProduto4 = Variaveis.fotoProduto4.Remove(0, 8);

                    txtCodigo.Text = Variaveis.codProduto.ToString();
                    txtNomeProduto.Text = Variaveis.nomeProduto;
                    txtDescricao.Text = Variaveis.descProduto;
                    txtTipo.Text = Variaveis.tipoProduto;
                    txtValor.Text = Variaveis.valorProduto.ToString();
                    mkdData.Text = Variaveis.dataCadProduto.ToString("dd/MM/yyyy");
                    cmbDestaque.Text = Variaveis.destaqueProduto;
                    cmbStatus.Text = Variaveis.statusProduto;
                    pctFoto01.Image = ByteToImage(GetImgToByte(Variaveis.enderecoServidorFtp + "produto/" + Variaveis.fotoProduto1));
                    pctFoto02.Image = ByteToImage(GetImgToByte(Variaveis.enderecoServidorFtp + "produto/" + Variaveis.fotoProduto2));
                    pctFoto03.Image = ByteToImage(GetImgToByte(Variaveis.enderecoServidorFtp + "produto/" + Variaveis.fotoProduto3));
                    pctFoto04.Image = ByteToImage(GetImgToByte(Variaveis.enderecoServidorFtp + "produto/" + Variaveis.fotoProduto4));

                }
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar os dados do produto! \n\n" + erro, "ERRO");
            }
        }

        private void CarregarProdutoCadastrado()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT idProduto FROM produto WHERE nomeProduto=@nome AND descProduto=@descricao";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                cmd.Parameters.AddWithValue("@nome", Variaveis.nomeProduto);
                cmd.Parameters.AddWithValue("@descricao", Variaveis.descProduto);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) { }
                Variaveis.codProduto = reader.GetInt32(0);
                txtCodigo.Text = Variaveis.codProduto.ToString();
                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao carregar o produto cadastrado! \n\n" + erro.Message, "ERRO");
            }
        }

        //////////////////////////////////////////////

        private void frmCadProduto_Load(object sender, EventArgs e)
        {
            pnlCadProduto.Location = new Point(this.Width / 2 - pnlCadProduto.Width / 2, this.Height / 2 - pnlCadProduto.Height / 2);

            lblUsuario.Text = "Bem-vindo(a) " + Variaveis.usuario;

            if (Variaveis.funcao == "ALTERAR")
            {
                lblTitulo.Text = "ALTERAÇÃO DO PRODUTO";
                CarregarDadosProduto();
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


        private void txtNomeProduto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDescricao.Focus();
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
               txtValor.Focus();
            }
        }

        private void txtValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtTipo.Focus();
            }
        }

        private void txtTipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cmbStatus.Focus();
            }
        }

        private void cmbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                cmbDestaque.Focus();
            }
        }

        private void cmbDestaque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                if (Variaveis.funcao == "CADASTRAR")
                {
                    mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                pctFoto01.Focus();
            }
        }

        private void pctFoto01_Click(object sender, EventArgs e)
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
                pctFoto01.Image = Image.FromFile(ofdFoto.FileName);
                Variaveis.fotoProduto1 = "produto/" + Path.GetFileName(ofdFoto.FileName);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Variaveis.atFotoProduto1 = "S";
                        Variaveis.caminhoFotoProduto1 = ofdFoto.FileName;
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show("Erro de segurança, fale com o administrador do sistema. \n Mensagem:\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Você não tem permissão." + ex.Message);
                    }
                    Variaveis.atFotoProduto1 = "S";
                    pctFoto02.Focus();
                }
            }
            catch
            {
                pctFoto02.Focus();
            }
        }

        private void pctFoto02_Click(object sender, EventArgs e)
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
                pctFoto02.Image = Image.FromFile(ofdFoto.FileName);
                Variaveis.fotoProduto2 = "produto/" + Path.GetFileName(ofdFoto.FileName);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Variaveis.atFotoProduto2 = "S";
                        Variaveis.caminhoFotoProduto2 = ofdFoto.FileName;
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show("Erro de segurança, fale com o administrador do sistema. \n Mensagem:\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Você não tem permissão." + ex.Message);
                    }
                    Variaveis.atFotoProduto2 = "S";
                    pctFoto03.Focus();
                }
            }
            catch
            {
                pctFoto03.Focus();
            }
        }

        private void pctFoto03_Click(object sender, EventArgs e)
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
                pctFoto03.Image = Image.FromFile(ofdFoto.FileName);
                Variaveis.fotoProduto3 = "produto/" + Path.GetFileName(ofdFoto.FileName);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Variaveis.atFotoProduto3 = "S";
                        Variaveis.caminhoFotoProduto3 = ofdFoto.FileName;
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show("Erro de segurança, fale com o administrador do sistema. \n Mensagem:\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Você não tem permissão." + ex.Message);
                    }
                    Variaveis.atFotoProduto3 = "S";
                    pctFoto04.Focus();
                }
            }
            catch
            {
                pctFoto04.Focus();
            }
        }

        private void pctFoto04_Click(object sender, EventArgs e)
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
                pctFoto04.Image = Image.FromFile(ofdFoto.FileName);
                Variaveis.fotoProduto4 = "produto/" + Path.GetFileName(ofdFoto.FileName);
                if (result == DialogResult.OK)
                {
                    try
                    {
                        Variaveis.atFotoProduto4 = "S";
                        Variaveis.caminhoFotoProduto4 = ofdFoto.FileName;
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show("Erro de segurança, fale com o administrador do sistema. \n Mensagem:\n" + ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Você não tem permissão." + ex.Message);
                    }
                    Variaveis.atFotoProduto4 = "S";
                    btnSalvar.Focus();
                }
            }
            catch
            {
                btnSalvar.Focus();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            lblNomeProduto.ForeColor = Color.FromArgb(73, 73, 73);
            lblDescricao.ForeColor = Color.FromArgb(73, 73, 73);
            lblValor.ForeColor = Color.FromArgb(73, 73, 73);
            lblTipo.ForeColor = Color.FromArgb(73, 73, 73);
            lblStatus.ForeColor = Color.FromArgb(73, 73, 73);
            lblDestaque.ForeColor = Color.FromArgb(73, 73, 73);
            lblDataCad.ForeColor = Color.FromArgb(73, 73, 73);
            lblFotoProduto01.BackColor = Color.FromArgb(133, 133, 134);
            lblFotoProduto01.ForeColor = Color.FromArgb(243, 243, 243);
            lblFotoProduto02.BackColor = Color.FromArgb(133, 133, 134);
            lblFotoProduto02.ForeColor = Color.FromArgb(243, 243, 243);
            lblFotoProduto03.BackColor = Color.FromArgb(133, 133, 134);
            lblFotoProduto03.ForeColor = Color.FromArgb(243, 243, 243);
            lblFotoProduto04.BackColor = Color.FromArgb(133, 133, 134);
            lblFotoProduto04.ForeColor = Color.FromArgb(243, 243, 243);

            if (txtNomeProduto.Text == String.Empty)
            {
                lblNomeProduto.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o nome do produto");
                txtNomeProduto.Focus();
            }
            else if (txtDescricao.Text == String.Empty)
            {
                lblDescricao.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher a descrição do produto");
                txtDescricao.Focus();
            }
            else if (txtValor.Text == String.Empty)
            {
                lblValor.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o valor do produto");
                txtValor.Focus();
            }
            else if (txtTipo.Text == String.Empty)
            {
                lblTipo.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o tipo do produto");
                txtTipo.Focus();
            }
            else if (cmbStatus.Text == String.Empty)
            {
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o status");
            }
            else if (cmbDestaque.Text == String.Empty)
            {
                lblDestaque.ForeColor = Color.Red;
                MessageBox.Show("Favor preencher o destaque");
            }
            else
            {
                Variaveis.nomeProduto = txtNomeProduto.Text;
                Variaveis.descProduto = txtDescricao.Text;
                Variaveis.tipoProduto = txtTipo.Text;
                Variaveis.valorProduto = Double.Parse(txtValor.Text);
                mkdData.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Variaveis.dataCadProduto = DateTime.Parse(mkdData.Text);
                Variaveis.destaqueProduto = cmbDestaque.Text;
                Variaveis.statusProduto = cmbStatus.Text;
                //Variaveis.fotoCliente = "cliente/" + nomeFoto;

                if (Variaveis.funcao == "CADASTRAR")
                {
                    InserirProduto();
                    CarregarProdutoCadastrado();
                }
                else if (Variaveis.funcao == "ALTERAR")
                {
                    AlterarProduto();
                    if (Variaveis.atFotoProduto1 == "S")
                    {
                        AlterarFotoProduto1();
                    }
                    if (Variaveis.atFotoProduto2 == "S")
                    {
                        AlterarFotoProduto2();
                    }
                    if (Variaveis.atFotoProduto3 == "S")
                    {
                        AlterarFotoProduto3();
                    }
                    if (Variaveis.atFotoProduto4 == "S")
                    {
                        AlterarFotoProduto4();
                    }
                }
                Variaveis.atFotoProduto1 = "N";
                Variaveis.atFotoProduto2 = "N";
                Variaveis.atFotoProduto3 = "N";
                Variaveis.atFotoProduto4 = "N";

                btnSalvar.Enabled = false;
                btnLimpar.Enabled = false;
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
    }
}
