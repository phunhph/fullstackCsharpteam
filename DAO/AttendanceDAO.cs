using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using fullstackCsharp.Models.ViewModel.Attendances;
using fullstackCsharp.Models;

namespace fullstackCsharp.DAO
{

    public class AttendanceDAO
    {
        public readonly string _connectString;


        public AttendanceDAO(IConfiguration configuration)
        {
            _connectString = configuration.GetConnectionString("sqlConnection");
        }
        public List<AttendancesTotalHours> SelectAt()
        {
            List<AttendancesTotalHours> attendances = new List<AttendancesTotalHours>();

            using (SqlConnection dbConnection = new SqlConnection(_connectString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_tblAttendance_Total", dbConnection))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    dbConnection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        AttendancesTotalHours atTotalHours = new AttendancesTotalHours();
                        atTotalHours.IdA = reader.GetInt32(0);
                        atTotalHours.IdU = reader.GetInt32(1);
                        atTotalHours.FullName = reader.GetString(2);
                        atTotalHours.CheckIn = reader.IsDBNull(3) ? TimeSpan.Zero : reader.GetTimeSpan(3);
                        atTotalHours.CheckOut = reader.IsDBNull(4) ? TimeSpan.Zero : reader.GetTimeSpan(4);
                        atTotalHours.totakwork = reader.GetDecimal(5);
                        attendances.Add(atTotalHours);

                    }
                    return attendances;
                }
            }

        }

        public List<TotalWorkMonth> TotalWorkMonths(SearchModel search)
        {
            List<TotalWorkMonth> totalWorks = new List<TotalWorkMonth>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("sp_tblAttendance_TotalMonth", sqlConnection))
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
                        TotalWorkMonth totalWorkMonth = new TotalWorkMonth();
                        totalWorkMonth.Idu = reader.GetInt32(0);
                        totalWorkMonth.Fullname = reader.GetString(1);
                        totalWorkMonth.totalworkmonth = reader.GetDecimal(2);
                        totalWorks.Add(totalWorkMonth);
                    }
                    return totalWorks;
                }
            }
        }
        public List<TotalWorkHoursSelf> TotalWorkHoursSelfs(int? year, int idu)
        {
            List<TotalWorkHoursSelf> totalWorksSelf = new List<TotalWorkHoursSelf>();
            using (SqlConnection sqlConnection = new SqlConnection(_connectString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("sp_tblTotalAttendanceMonth_self", sqlConnection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    if (year.HasValue)
                    {
                        sqlCommand.Parameters.AddWithValue("@year", year.Value);
                    }
                    else
                    {
                        sqlCommand.Parameters.AddWithValue("@year", DBNull.Value);
                    }
                    sqlCommand.Parameters.AddWithValue("@idu", idu);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        TotalWorkHoursSelf workHoursSelf = new TotalWorkHoursSelf();
                        workHoursSelf.Idu = reader.GetInt32(0);
                        workHoursSelf.FullName = reader.GetString(1);
                        workHoursSelf.Month = reader.GetInt32(2);
                        workHoursSelf.TotalWorkMonth = reader.GetDecimal(3);
                        totalWorksSelf.Add(workHoursSelf);
                    }
                    return totalWorksSelf;
                }
            }
        }

    }
}
