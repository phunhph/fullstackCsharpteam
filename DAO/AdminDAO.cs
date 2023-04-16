using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using fullstackCsharp.Models;

public class AdminDAO
{
    static string connString = "Data Source=DESKTOP-HOA8KMR;Initial Catalog=QuanLy1;Integrated Security=True;TrustServerCertificate=true";
    public List<Admin> Select(string manv)
    {
        List<Admin> employeeList = new List<Admin>();
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "SELECT * FROM NhanVien WHERE ID_NV=@manv";
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                // set param
                command.Parameters.Add("@manv", SqlDbType.VarChar).Value = manv;
                // load

                // nếu là select
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeList.Add(new Admin((string)reader[0], (string)reader[1], (string)   reader[2]));
                        // return danh sách nhân viên ở đây
                        Console.WriteLine(String.Format("{0}", reader[0]));
                    }
                }
                // nếu là insert, update, delete
                // command.ExecuteNonQuery(); 

                // vì là select nên không thay đổi gì db, sau khi select xong thì rollback cho chắc
                transaction.Rollback();
                // nếu là insert, update, delete thì dùng: transaction.Commit();

            }
            catch
            {
                // Nếu có lỗi xảy ra thì rollback để tránh làm mất dữ liệu DB
                transaction.Rollback();
                throw;
            }
        }

        return employeeList;
    }
}