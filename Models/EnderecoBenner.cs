using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
namespace CadastroPaciente.Models
{
    public class EnderecoBenner
    {
        public string endereco { get; set; }
        public string tel1 { get; set; }
        public string tel2 { get; set; }
        public string cel { get; set; }
        public string fax { get; set; }

        public EnderecoBenner()
        {

        }

         EnderecoBenner(string pEndereco,string pTel1,string pTel2,string pCel,string pfax)
        {
            this.endereco = pEndereco;
            this.tel1 = pTel1;
            this.tel2 = pTel2;
            this.cel = pCel;
            this.fax = pfax;
        }

        public List<EnderecoBenner> lista(string pPaciente)
        {
            List<EnderecoBenner> _enderecobenner = new List<EnderecoBenner>();

            Conexao con = new Conexao();

            if (con.Abrir(2))
            {
                try { 
                OracleCommand cm = new OracleCommand();
                cm.CommandText = "Select * from saudepro.PLANO_V_DADOS_PAC_ENDERECO t where t.cd_paciente = :paciente";
                    cm.Parameters.Add( new OracleParameter("paciente",OracleDbType.Varchar2));
                cm.Parameters["paciente"].Value = pPaciente;
                cm.Connection = con.conexao;

                OracleDataReader rd = cm.ExecuteReader();

                if (rd.HasRows)
                {
                    while (rd.Read()){
                        _enderecobenner.Add(new EnderecoBenner(rd.GetOracleString(2).ToString(),
                                                                rd.GetOracleString(4).ToString(), 
                                                                rd.GetOracleString(5).ToString(), 
                                                                rd.GetOracleString(6).ToString(),
                                                                rd.GetOracleString(7).ToString()));
                    }
                }
                } catch
                {
                    throw;
                }
                con.Fechar();
            }


            return _enderecobenner;
        }
    }
}
