using CadastroPaciente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CadastroPaciente.Controllers
{
    public class BeneficiarioBenner
    {
        public List<string> emails { get; set; }
        
        public string matricula { get; set; }
        public BeneficiarioBenner()
        {

        }
        public BeneficiarioBenner(string pCdPacinte)
        {
            this.emails = this.BuscaEmails(pCdPacinte);
        }

        private List<string> BuscaEmails(string pcdPaciente )
        {
            List<string> emails = new List<string>();

            Conexao con = new Conexao();
            if (con.Abrir(2))
            {
                OracleCommand cm = new OracleCommand();
                cm.Connection = con.conexao;
                cm.CommandText = "SELECT * FROM SAUDEPRO.PLANO_V_DADOS_CONSULTA_BENEF_EMAIL B WHERE B.CD_PACIENTE = " + pcdPaciente;
                OracleDataReader rd ;
                rd = cm.ExecuteReader();

                if(rd.HasRows)
                {
                    while (rd.Read())
                    {
                        emails.Add(rd.GetOracleString(2).ToString());
                        this.matricula = rd.GetOracleString(1).ToString();
                    }
                }

            }

            return emails;
        }



    }
}
