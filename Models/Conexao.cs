using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace CadastroPaciente.Models
{
    public class Conexao
    {
        public OracleConnection conexao { get; set; }
        string connnection_string_MV = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.11)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=HMSCDB)));User Id=abfsc;Password=#bf$c2021;";
        //string connnection_string_Benner = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.1.38)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=PCORP01)));User Id=saudepro;Password=saude#planopro;";
        string connnection_string_Benner = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 172.16.1.38)(PORT = 1521)))(CONNECT_DATA = (SID = PCORP01)));Persist Security Info=True;User ID=saudepro;Password=saude#planopro;Pooling=True;Connection Timeout=60;";
        public Boolean Abrir(long pSistema)
        {
            try { 
            this.conexao = new OracleConnection(pSistema == 1 ? connnection_string_MV : connnection_string_Benner);
                this.conexao.Open();
                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public void Fechar()
        {
            if(conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
            
        }
    }
}
