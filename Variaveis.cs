using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monisePerso
{
    public static class Variaveis
    {
        //Geral
        public static string funcao;
        public static int linhaSelecionada, linhaFoneSelecionada;

        //Login
        public static string usuario, senha, nivel;

        //Contato
        public static int codContato;

        //Empresa 
        public static int codEmpresa;
        public static string nomeEmpresa, cnpjCpf, razaoSocial, emailEmpresa, statusEmpresa;
        public static DateTime dataCadEmpresa, horarioAtendEmpresa;

        //Cliente
        public static int codCliente;
        public static string nomeCliente, emailCliente, senhaCliente, statusCliente, fotoCliente, caminhoFotoCliente, atFotoCliente;
        public static DateTime dataCadCliente;

        //Funcionario
        public static int codFuncionario;
        public static string nomeFuncionario, emailFuncionario, senhaFuncionario, nivelFuncionario, statusFuncionario;
        public static DateTime dataCadFuncionario, horarioFuncionario;

        //FoneEmpresa
        public static int codFoneEmpresa;
        public static string numeroEmpresa, operFoneEmpresa, descFoneEmpresa;

        //FoneCliente
        public static int codFoneCliente;
        public static string numeroCliente, operFoneCliente, descFoneCliente;

        //FoneFuncionario
        public static int codFoneFuncionario;
        public static string numeroFuncionario, operFoneFuncionario, descFoneFuncionario;

        //Produtos
        public static int codProduto;
        public static string nomeProduto, descProduto, tipoProduto,atFotoProduto1, atFotoProduto2, atFotoProduto3, atFotoProduto4, fotoProduto1, fotoProduto2, fotoProduto3, fotoProduto4, caminhoFotoProduto1, caminhoFotoProduto2, caminhoFotoProduto3, caminhoFotoProduto4, destaqueProduto, statusProduto;
        public static DateTime dataCadProduto;
        public static double valorProduto;

        //FOTOS FTP
        public static string enderecoServidorFtp = "ftp://127.0.0.1/admin/";
        public static string usuarioFtp = "persomonise";
        public static string senhaFtp = "123";
    }
}
