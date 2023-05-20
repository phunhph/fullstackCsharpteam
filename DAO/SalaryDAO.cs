using fullstackCsharp.Models;
using fullstackCsharp.Models.ViewModel.Salaries;
using Microsoft.Data.SqlClient;
using System.Data;

namespace fullstackCsharp.DAO
{
    public class SalaryDAO
    {
        public readonly string _connectString;
        public SalaryDAO(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("sqlConnection");
        }

        public List<TotalSalaryViewModel> totalSalaryViewModels(SearchModel search)
        {
            List<TotalSalaryViewModel> totaSalarys = new List<TotalSalaryViewModel>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("sp_totalSalaryMonth", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (search.Month.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@month", search.Month.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@month", DBNull.Value);
                    }
                    if (search.Year.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@year", search.Year.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@year", DBNull.Value);
                    }
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        TotalSalaryViewModel totalSalary = new TotalSalaryViewModel();
                        totalSalary.IdU = reader.GetInt32(0);
                        totalSalary.FullName = reader.GetString(1);
                        totalSalary.UserName = reader.GetString(2);
                        totalSalary.RealityWork = reader.GetDecimal(3);
                        totalSalary.Month = reader.GetInt32(4);
                        totalSalary.Year = reader.GetInt32(5);
                        totalSalary.FullWorkMonth = reader.GetDecimal(6);
                        totalSalary.BasicSalary = reader.GetString(7);
                        totalSalary.Coefficient = reader.GetDecimal(8);
                        totalSalary.Salary = reader.GetString(9);
                        totalSalary.AllowanceAmount = reader.GetString(10);
                        totalSalary.TotalPayOff = reader.GetString(11);
                        totalSalary.TotalSalaryMonth = reader.GetString(12);
                        totaSalarys.Add(totalSalary);
                    }
                    return totaSalarys;
                }
            }
        }

        public List<TotalSalaryViewModel> totalSalarySelf(SearchModel search,int idu)
        {
            List<TotalSalaryViewModel> totaSalarys = new List<TotalSalaryViewModel>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("sp_totalSalarySelf", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    if (search.Month.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@month", search.Month.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@month", DBNull.Value);
                    }
                    if (search.Year.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@year", search.Year.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@year", DBNull.Value);
                    }
                    sqlCommand.Parameters.AddWithValue("@idu", idu);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        TotalSalaryViewModel totalSalary = new TotalSalaryViewModel();
                        totalSalary.IdU = reader.GetInt32(0);
                        totalSalary.FullName = reader.GetString(1);
                        totalSalary.UserName = reader.GetString(2);
                        totalSalary.RealityWork = reader.GetDecimal(3);
                        totalSalary.Month = reader.GetInt32(4);
                        totalSalary.Year = reader.GetInt32(5);
                        totalSalary.FullWorkMonth = reader.GetDecimal(6);
                        totalSalary.BasicSalary = reader.GetString(7);
                        totalSalary.Coefficient = reader.GetDecimal(8);
                        totalSalary.Salary = reader.GetString(9);
                        totalSalary.AllowanceAmount = reader.GetString(10);
                        totalSalary.TotalPayOff = reader.GetString(11);
                        totalSalary.TotalSalaryMonth = reader.GetString(12);
                        totaSalarys.Add(totalSalary);

                    }
                    if (totaSalarys.Count == 0)
                    {
                        TotalSalaryViewModel totalSalary = new TotalSalaryViewModel();
                        totalSalary.IdU = idu;
                        totalSalary.FullName = "fullname";
                        totalSalary.RealityWork = 0;
                        totalSalary.Month = search.Month;
                        totalSalary.Year = search.Year;
                        totalSalary.FullWorkMonth = 0;
                        totalSalary.BasicSalary = "0";
                        totalSalary.Coefficient = 0;
                        totalSalary.Salary ="0";
                        totalSalary.AllowanceAmount ="0";
                        totalSalary.TotalPayOff ="0";
                        totalSalary.TotalSalaryMonth ="0";
                        totaSalarys.Add(totalSalary);
                        // Các thuộc tính khác của totalSalary được đặt thành giá trị mặc định 0 hoặc null tùy vào kiểu dữ liệu.
                        // Gán giá trị 0 hoặc null cho các thuộc tính không có kết quả.
                    }

                    return totaSalarys;
                }
            }
        }
    }
}
