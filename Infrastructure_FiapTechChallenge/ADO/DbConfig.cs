using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure_FiapTechChallenge.ADO
{
    public class DbConfig : IDisposable
    {
        public readonly SqlConnection connection;

        public DbConfig()
        {

            connection = new SqlConnection("Server=localhost,30003;Database=DBFiap;User ID=sa;Password=1q2w3e4r@#;Trusted_Connection=False; TrustServerCertificate=True;");
            connection.Open();

        }

        public async Task<DataTable> ExecProcedure(SqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var reader = await cmd.ExecuteReaderAsync();
                dt.Load(reader);

                return dt;

            }
            catch 
            {
                return new DataTable();
            }
        }

        public void Dispose() 
        {
        
        }
    }
}
