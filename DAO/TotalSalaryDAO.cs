using Microsoft.Data.SqlClient;

namespace fullstackCsharp.DAO
{
    public class TotalSalaryDAO
    {
        private readonly IConfiguration _configuration;
        public TotalSalaryDAO(IConfiguration configuration) { 
            _configuration= configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("sqlConnection"));
        }

       // public List
    }
}
