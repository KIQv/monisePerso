﻿using MySql.Data.MySqlClient;
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
    public partial class frmEmail : Form
    {
        public frmEmail()
        {
            InitializeComponent();
        }

        private void CarregarContato()
        {
            try
            {
                banco.Conectar();
                string selecionar = "SELECT * FROM `contato`";
                MySqlCommand cmd = new MySqlCommand(selecionar, banco.conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvEmails.DataSource = dt;

                dgvEmails.ClearSelection();

                banco.Conectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao selecionar o contato. \n\n" + erro.Message);
            }
        }

        private void ExcluirContato()
        {
            try
            {
                banco.Conectar();
                string excluir = "DELETE FROM `contato` WHERE `idContato`=@codigo";
                MySqlCommand cmd = new MySqlCommand(excluir, banco.conexao);
                cmd.Parameters.AddWithValue("@codigo", Variaveis.codCliente);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvEmails.DataSource = dt;

                dgvEmails.ClearSelection();

                banco.Desconectar();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao excluir o contato. \n\n" + erro.Message);
            }
        }
        private void frmEmail_Load(object sender, EventArgs e)
        {
            pnlEmails.Location = new Point(this.Width / 2 - pnlEmails.Width / 2, this.Height / 2 - pnlEmails.Height / 2);

            Variaveis.linhaSelecionada = -1;
            CarregarContato();


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


        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (Variaveis.linhaSelecionada >= 0)
            {
                var resultado = MessageBox.Show("Deseja realmente excluir?", "EXCLUIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    ExcluirContato();
                }
            }
            else
            {
                MessageBox.Show("Para excluir selecione uma linha");
            }
        }

        private void dgvEmails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Variaveis.linhaSelecionada = int.Parse(e.RowIndex.ToString());
            if (Variaveis.linhaSelecionada >= 0)
            {
                Variaveis.codCliente = Convert.ToInt32(dgvEmails[0, Variaveis.linhaSelecionada].Value);
            }
        }

        private void dgvEmails_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvEmails.Sort(dgvEmails.Columns[1], ListSortDirection.Ascending);
            dgvEmails.ClearSelection();
        }
    }
}