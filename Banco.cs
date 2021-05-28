using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace CFB_Academia
{
    class Banco
    {
        private static SQLiteConnection conexao;
        //Funções Genericas

        public static DataTable dql(string sql)//Data Query Language (SELECT)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = sql;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void dml(string q, string msgOK=null, string msgERRO = null)//Data Manipulation Language (INSERT, DELETE, UPDATE)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = q;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();
                if (msgOK != null)
                {
                    MessageBox.Show(msgOK);
                }

            }
            catch (Exception ex)
            {
                if (msgERRO!= null)
                {
                    MessageBox.Show(msgERRO + "\n" + ex.Message);
                }
                {

                }
                throw ex;
            }
        }
        private static SQLiteConnection ConexaoBanco()
        {
            conexao = new SQLiteConnection("Data Source =" + Globais.caminhoBanco + Globais.nomeBanco);
            conexao.Open();
            return conexao;
        }

        public static DataTable ObterTodosUsuarios()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "SELECT * FROM tb_usuarios";
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;
            
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        

        ///////// Funções do FORM F_NOVOUSUARIO
        
        public static void NovoUsuario(Usuario u)
        {
            if (ExisteUsername(u))
            {
                MessageBox.Show("Username já existe");
                return;
            }
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "INSERT INTO tb_usuarios(T_NOMEUSUARIO, T_USERNAME, T_SENHAUSUARIO, T_STATUSUSUARIO, N_NIVELUSUARIO) VALUES (@T_NOMEUSUARIO, @T_USERNAME, @T_SENHAUSUARIO, @T_STATUSUSUARIO,@N_NIVELUSUARIO )";
                cmd.Parameters.AddWithValue("@T_NOMEUSUARIO", u.T_NOMEUSUARIO);
                cmd.Parameters.AddWithValue("@T_USERNAME", u.T_USERNAME);
                cmd.Parameters.AddWithValue("@T_SENHAUSUARIO", u.T_SENHAUSUARIO);
                cmd.Parameters.AddWithValue("@T_STATUSUSUARIO", u.T_STATUSUSUARIO);
                cmd.Parameters.AddWithValue("@N_NIVELUSUARIO", u.N_NIVELUSUARIO);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Novo Usuário Cadastrado!");
                vcon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gravar novo usuário" + ex.Message);
            }

        }

        /// FIM  - Funções do FORM F_NOVOUSUARIO

        /// Funções do FORM F_GestãoUsuarios


        public static DataTable ObterUsuariosIdNome()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "SELECT N_IDUSUARIO as 'ID Usuário', T_NOMEUSUARIO as 'Nome Usuário' FROM tb_usuarios";
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable ObterDadosUsuario(string id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "SELECT * FROM tb_usuarios WHERE N_IDUSUARIO =" + id;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                da.Fill(dt);
                vcon.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void AtualizarDadosUsuario(Usuario u)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "UPDATE tb_usuarios SET T_NOMEUSUARIO = '" + u.T_NOMEUSUARIO +"',T_USERNAME = '"+u.T_USERNAME+"',T_SENHAUSUARIO = '"+u.T_SENHAUSUARIO+"',T_STATUSUSUARIO = '"+u.T_STATUSUSUARIO+"',N_NIVELUSUARIO = "+u.N_NIVELUSUARIO+" WHERE N_IDUSUARIO = " + u.N_IDUSUARIO;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void DeletarUsuario(string id)
        {
            SQLiteDataAdapter da = null;

            try
            {
                var vcon = ConexaoBanco();
                var cmd = vcon.CreateCommand();
                cmd.CommandText = "DELETE FROM tb_usuarios WHERE N_IDUSUARIO=" + id;
                da = new SQLiteDataAdapter(cmd.CommandText, vcon);
                cmd.ExecuteNonQuery();
                vcon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// FIM  - Funções do FORM F_GestãoUsuarios


        /// ROTINAS GERAIS  
        public static bool ExisteUsername (Usuario u)
        {
            bool res;
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();

            var vcon = ConexaoBanco();
            var cmd = vcon.CreateCommand();
            cmd.CommandText = "SELECT T_USERNAME FROM tb_usuarios WHERE T_USERNAME = '" + u.T_USERNAME + "'";

            da = new SQLiteDataAdapter(cmd.CommandText, vcon);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            vcon.Close();
            return res;
        }

    }
}
