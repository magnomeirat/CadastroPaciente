using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
    

namespace CadastroPaciente.Models
{
    public class Usuario
    {
        public string usuario { get; set; }
        public string senha { get; set; }
        public Usuario()
        {

        }
        public Usuario(string pUsuario,string pSenha)
        {
            this.usuario = pUsuario;
            this.senha = pSenha;
        }
       public bool autenticar()
        {
            Conexao con = new Conexao();
            bool autenticado = false;
            if (con.Abrir(1))
            {
                OracleCommand cm = new OracleCommand();
                cm.CommandText = "SELECT dbasgu.fnc_mv2000_hmvpep(:USUARIO, :SENHA) FROM DUAL";
                cm.Parameters.Add(new OracleParameter("USUARIO", OracleDbType.Varchar2));
                cm.Parameters.Add(new OracleParameter("SENHA", OracleDbType.Varchar2));
                cm.Parameters["USUARIO"].Value = this.usuario;
                cm.Parameters["SENHA"].Value = this.senha;
                cm.Connection = con.conexao;

                OracleDataReader rd = cm.ExecuteReader();

                if(rd.HasRows)
                    {

                    rd.Read();
                    if (rd.GetString(0) != "USUARIO NAO CADASTRADO" && rd != null && rd.GetString(0) != "SENHA INVALIDA")
                    {
                        autenticado = true;
                    }

                    } 
                else
                {
                    autenticado = false;
                }
                con.Fechar();
            }

            return autenticado;
        }
    }
}
