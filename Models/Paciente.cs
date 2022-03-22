using CadastroPaciente.Controllers;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPaciente.Models
{
    public class Paciente
    {
        public string cd_paciente { get; set; }
        public string nome { get; set; }
        public string data_nascimento { get; set; }
        public string nome_mae { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string e_mail { get; set; }
        public string endereco { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string celular { get; set; }
        public BeneficiarioBenner dadosBenner { get; set; }

        public List<EnderecoBenner> enderecosBenner { get; set; }
        public List<TelefonesMV> telefonesMV {get;set;}

        public Paciente()
        {

        }

        public Paciente(string pCdPaciente, string pNome, string pNome_mae, string pdataNascimento)
        {
            this.cd_paciente = pCdPaciente;
            this.nome = pNome;

            this.nome_mae = pNome_mae;
            this.data_nascimento = pdataNascimento;
            

           

        }

        public Paciente(string pCdPaciente, string pNome, string pNome_mae, string pdataNascimento,string pEmail, string pEndereco,string pCelular)
        {
            this.cd_paciente = pCdPaciente;
            this.nome = pNome;

            this.nome_mae = pNome_mae;
            this.data_nascimento = pdataNascimento;
            this.e_mail = pEmail;
            this.endereco = pEndereco;
            this.celular = pCelular;
            this.dadosBenner = new BeneficiarioBenner(this.cd_paciente);

            EnderecoBenner _enderecosBenner = new EnderecoBenner();
            this.enderecosBenner = _enderecosBenner.lista(this.cd_paciente);

            TelefonesMV _telefones = new TelefonesMV();
            this.telefonesMV = _telefones.listar(this.cd_paciente);

        }

        public List<Paciente> Listar(string tipo, string pPesquisa)
        {
            List<Paciente> lista = new List<Paciente>();
            Conexao con = new Conexao();
            if (con.Abrir(1)){

                OracleCommand cm = new OracleCommand();

                string sql = "select * from abfsc.hmsc_v_cadastrobenef_tela " + 
                             " WHERE ";
                
               switch(tipo)
                { 
                    case "1": sql += " CD_PACIENTE = " + pPesquisa ;    break;
                    case "2": sql += " upper(NM_PACIENTE)  LIKE upper('%" + pPesquisa + "%')"; break;
                    case "3": sql += " nr_cpf ='" + pPesquisa + "'"; break;

                }

                sql += " and dt_nascimento is not null";
                

                    



                    cm.CommandText = sql;


                    
                cm.Connection = con.conexao;

                try
                {
                    OracleDataReader rd = cm.ExecuteReader();

                    if (rd.HasRows)
                    {
                        while (rd.Read()) {

                            
                            lista.Add(new Paciente(
                                rd.GetOracleString(0).ToString(),

                                rd.GetOracleString(1).ToString(),

                                 
                                rd.GetOracleString(2).ToString(),

                                rd.GetDateTime(3).ToString("dd/MM/yyyy")





                                )
                                
                                );
                        }
                    }
                }catch(Exception ex) {
                    throw new InvalidOperationException(ex.Message);
                }
                con.Fechar();

            }

            return lista;

        }


        public Paciente Ver(string pPesquisa)
        {


            Conexao con = new Conexao();
            Paciente paciente = new Paciente();
            string sql = "select * from abfsc.hmsc_v_cadastrobenef_tela  " +
                        "WHERE ";
                   sql += " CD_PACIENTE = :pesquisa" ;
                   sql += " and dt_nascimento is not null";

            if (con.Abrir(1))
            {
                OracleCommand cm = new OracleCommand();

                cm.CommandText = sql;
                cm.Parameters.Add(new OracleParameter("pesquisa", OracleDbType.Varchar2));
                cm.Parameters["pesquisa"].Value = pPesquisa;
                cm.Connection = con.conexao;

                try
                {
                    OracleDataReader rd = cm.ExecuteReader();

                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {


                            paciente = new Paciente(
                                rd.GetOracleString(0).ToString(),

                                rd.GetOracleString(1).ToString(),


                                rd.GetOracleString(2).ToString(),

                                rd.GetDateTime(3).ToString("dd/MM/yyyy"),
                                rd.GetOracleString(4).ToString(),
                                rd.GetOracleString(6).ToString(),
                                rd.GetOracleString(7).ToString()

                                );
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
                con.Fechar();

            }

            return paciente;

        }
    }
}
