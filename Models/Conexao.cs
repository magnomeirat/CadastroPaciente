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
        string connnection_string_MV = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=999.99.99.99)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=instancia)));User Id=usuario;Password=senha;";
        
        string connnection_string_Benner = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = 999.99.9.99)(PORT = 1521)))(CONNECT_DATA = (SID = instancia)));Persist Security Info=True;User ID=usuario;Password=senha;Pooling=True;Connection Timeout=60;";
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
