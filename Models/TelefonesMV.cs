using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CadastroPaciente.Models
{
    public class TelefonesMV
    {
        public string telefone { get; set; }
        public string tipo { get; set; }
        public TelefonesMV()
        {

        }

        public TelefonesMV(string pTelefone,string pTipo)
        {
            this.telefone = pTelefone;
            this.tipo = pTipo;
        }

        public List<TelefonesMV> listar(string pPaciente)
        {
            List<TelefonesMV> _telefones = new List<TelefonesMV>();

            Conexao con = new Conexao();

            if (con.Abrir(1))
            {
                try
                {
                    OracleCommand cm = new OracleCommand();
                    cm.Connection = con.conexao;
                    cm.CommandText = "SELECT * FROM ABFSC.HMSC_V_TELEFONES_PAC T WHERE T.CD_PACIENTE = :PACIENTE";
                    cm.Parameters.Add(new OracleParameter("PACIENTE", OracleDbType.Varchar2));
                    cm.Parameters["PACIENTE"].Value = pPaciente;

                    OracleDataReader rd = cm.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            _telefones.Add(new TelefonesMV(rd.GetString(1).ToString(),
                                                           rd.GetString(2).ToString()));
                        }
                    }

                }
                catch
                {
                    throw;
                }

                con.Fechar();
            }

            return _telefones;
        }
    }
}
