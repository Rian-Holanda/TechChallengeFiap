using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Medico.Command;
using DataAccess_TechChallengeFiap.Repository;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.ADO;
using Infrastructure_FiapTechChallenge.Util;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_TechChallengeFiap.Consultas.Queries
{
    public class ConsultaQueries : IConsultaQueries
    {
        private readonly IAppDbContext context;
        private readonly ILogger<ConsultaQueries>? logger;
        public ConsultaQueries(IAppDbContext context, ILogger<ConsultaQueries>? logger)
        {
            this.context = context;
            this.logger = logger;
        }

        Util util = new Util();

        public List<ListaHorarioDias> GetHorariosDias() 
        {
            try 
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ListaHorariosDia", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt =  _dbConfig.ExecProcedure(cmd);

                        var lista = util.ConvertDataTable<ListaHorarioDias>(dt);

                        return lista;
                    }
                }
            }
            catch 
            { 
                return new List<ListaHorarioDias>();
            }
            
        }

        public  List<ConsultasMedico> GetConsultasDisponiveisMedico(int idMedico, DateTime dataConsulta, string dia)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ListaConsultasDisponiveisMedico", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.Parameters.AddWithValue("@DataConsulta", dataConsulta);
                        cmd.Parameters.AddWithValue("@Dia", dia);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = _dbConfig.ExecProcedure(cmd);
                        
                        var lista = util.ConvertDataTable<ConsultasMedico>(dt);

                        return lista;
                    }
                }
            }
            catch
            {
                return new List<ConsultasMedico>();
            }
        }

        public  List<Consulta> GetHorariosConsultas(int idMedico)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_HorariosConsultas", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = _dbConfig.ExecProcedure(cmd);

                        var lista = util.ConvertDataTable<Consulta>(dt);

                        return lista;
                    }
                }
            }
            catch
            {
                return new List<Consulta>();
            }
        }

        public List<Consulta> GetConsultasMedico(int idMedico)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasMedico", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = _dbConfig.ExecProcedure(cmd);

                        var lista = util.ConvertDataTable<Consulta>(dt);

                        return lista;
                    }
                }
            }
            catch
            {
                return new List<Consulta>();
            }
        }

        public List<Consulta> GetConsultasPaciente(int idPaciente)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig())
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasPaciente", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = _dbConfig.ExecProcedure(cmd);

                        var lista = util.ConvertDataTable<Consulta>(dt);

                        return lista;
                    }
                }
            }
            catch
            {
                return new List<Consulta>();
            }
        }

    }
}
