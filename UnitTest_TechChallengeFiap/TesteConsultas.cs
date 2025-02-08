using DataAccess_TechChallengeFiap.Consultas.Queries;
using DataAccess_TechChallengeFiap.Repository;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.ADO;
using Infrastructure_FiapTechChallenge.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest_TechChallengeFiap
{
    public class TesteConsultas
    {
        //using the same connection string
        public static string connectionString = $"Data Source=localhost,30003;Initial Catalog=DBFiap;Persist Security Info=True;User ID=sa;Password=1q2w3e4r@#;TrustServerCertificate=true";



        [Fact]
        public void ValidaAcessoConsultas()
        {

            SqlConnection sqlConnection = new SqlConnection (connectionString);

            Util util = new Util();

            try 
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ListaHorariosDia", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = _dbConfig.ExecProcedure(cmd);

                        var lista = util.ConvertDataTable<ListaHorarioDias>(dt);

                        if (lista.Count <= 0)
                            throw new Exception("Erro");
                    }
                }
            }
            catch (Exception ex) 
            { 
            
            }
            
        }
    }

}

