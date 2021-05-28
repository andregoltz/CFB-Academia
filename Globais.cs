using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFB_Academia
{
    class Globais
    {
        public static string versao = "1.0";
        public static bool logado = false;
        public static int nivel = 0;//1 = Básico, 2 = Gerente, 3 = Desenvolvedor
        //public static string caminho = System.Environment.CurrentDirectory;
        public static string caminho = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
        public static string nomeBanco = "banco_academia.db";
        public static string caminhoBanco = caminho + @"\banco\";
        public static string caminhoFotos = caminho + @"\Foto\";

        /*
         * 
         DADOS DE tb_usuarios
         * 
        N_IDUSUARIO
        T_NOMEUSUARIO
        T_USERNAME
        T_SENHAUSUARIO
        T_STATUSUSUARIO
        N_NIVELUSUARIO
         */
    }
}
