using DataAccess_TechChallengeFiap.Consultas.Interface;
using DataAccess_TechChallengeFiap.Medico.Command;
using Entity_TechChallengeFiap.Entities;
using Infrastructure_FiapTechChallenge;
using Infrastructure_FiapTechChallenge.ADO;
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
        private readonly string connection;
        public ConsultaQueries(IAppDbContext context, ILogger<ConsultaQueries>? logger, string connection)
        {
            this.context = context;
            this.logger = logger;
            this.connection = connection;
        }

        public async Task<DataTable> GetHorariosDias() 
        {
            try 
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ListaHorariosDia", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return  dt;
                    }
                }
            }
            catch 
            { 
                return new DataTable();
            }
            
        }

        public async Task<DataTable> GetConsultasDisponiveisMedico(int idMedico)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasDisponiveisMedico", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetHorariosConsultas()
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_HorariosConsultas", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetConsultasMedico(int idMedico)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasMedico", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetConsultasPaciente(int idPaciente)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasPaciente", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetConsultasMedicos()
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasMedicos", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetConsultasPacientes()
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_ConsultasPacientes", _dbConfig.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }

        public async Task<DataTable> GetHistoricoConsulta(int idConsulta)
        {
            try
            {
                using (DbConfig _dbConfig = new DbConfig(connection))
                {
                    using (SqlCommand cmd = new SqlCommand("PRC_HistoricoConsulta", _dbConfig.connection))
                    {
                        cmd.Parameters.AddWithValue("@idConsulta", idConsulta);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var dt = await _dbConfig.ExecProcedure(cmd);

                        return dt;
                    }
                }
            }
            catch
            {
                return new DataTable();
            }
        }
    }
}
