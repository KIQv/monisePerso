using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monisePerso
{
    public static class banco
    {
        // String de conexão com o banco de dados
        // public static string db = "SERVER=ipservidor;USER=nome_usuario;PASSWORD=senha;DATABASE=persomonise;SSL MODE=none";
        public static string db = "SERVER= 45.152.46.154;PORT=3306;DATABASE=u700303309_persomonise;UID=u700303309_moniseperso;PASSWORD=Kfsar@25790";

        // Reconhecer a biblioteca MySql
        public static MySqlConnection conexao;

        // Método para abrir a conexão
        public static void Conectar()
        {
            try //Parecido com o if porem ele realiza todos os comandos sem pergunta, além de caso haja algum erro ele pula direto para o atch
            {
                conexao = new MySqlConnection(db);
                conexao.Open();
            }
            catch
            {
                MessageBox.Show("Erro ao conectar com o Banco de Dados");
            }
        }

        // Método para fechar a conexão
        public static void Desconectar()
        {
            try //Parecido com o if porem ele realiza todos os comandos sem pergunta, além de caso haja algum erro ele pula direto para o atch
            {
                conexao = new MySqlConnection(db);
                conexao.Close();
            }
            catch
            {
                MessageBox.Show("Erro ao se desconectar ao Banco de Dados");
            }
        }
    }
}