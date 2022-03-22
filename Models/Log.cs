using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CadastroPaciente.Models
{
    public class Log
    {
      

        public static void salvar(string ppaciente,string pusuario)
        {


            Conexao con = new Conexao();

            if (con.Abrir(1)) { 
            OracleCommand cm = new OracleCommand();

            cm.CommandText = "insert into abfsc.HMSC_LOG_CONSULT_PAC_TELA(CD_LOG_CONSULT_PAC_TELA,usuario,cd_paciente,dataconsulta) " +
                "values (SEQ_HMSC_LOG_CONSULT_PAC_TELA.nextval,:usuario,:paciente,sysdate)";

                cm.Parameters.Add(new OracleParameter("usuario", OracleDbType.Varchar2));
                cm.Parameters.Add(new OracleParameter("paciente", OracleDbType.Varchar2));

                cm.Parameters["usuario"].Value = pusuario;
                cm.Parameters["paciente"].Value = ppaciente;

                try { 

                cm.Connection = con.conexao;
                cm.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
                finally { 
                con.Fechar();
                }

            }





        }
    }
}
