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

            connection = new SqlConnection("Data Source=localhost,30003;Initial Catalog=DBFiap;Persist Security Info=True;User ID=sa;Password=1q2w3e4r@#;TrustServerCertificate=true");
            connection.Open();

        }

        public DataTable ExecProcedure(SqlCommand cmd)
        {
            try
            {
                DataTable dt = new DataTable();

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                var reader = cmd.ExecuteReader();
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
