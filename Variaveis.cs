using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monisePerso
{
    internal class Variaveis
    {
        //Geral
        public static string funcao;
        public static int linhaSelecionada, linhaFoneSelecionada;

        //Login
        public static string usuario, senha;

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
    }
}
