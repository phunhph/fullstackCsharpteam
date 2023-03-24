using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using fullstackCsharp.Models;

public class LoginDAO
{
    static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";
    /* static string connString = "Server=tcp:employee-management.database.windows.net,1433;Initial Catalog=employee-management;Persist Security Info=False;User ID=quyethieuphu;Password=Employee-management;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";*/
    public bool ValidateUser(Login Login)
    {
        bool result = false;
        using (SqlConnection connection = new SqlConnection(connString))
        {
            // truy vấn acc
            string query = "SELECT  COUNT(*) FROM NhanVien WHERE username=@username AND passwords=@password";
            SqlCommand command = new SqlCommand(query, connection);
            // truyền  tham số vào truy vấn và tự động xác định kiểu dữ liệu
            command.Parameters.AddWithValue("@username", Login.user);
            command.Parameters.AddWithValue("@password", Login.password);
            connection.Open();
            // lưu số lượng acc truy vấn ra được đúng đk
            int count = (int)command.ExecuteScalar();
            if (count > 0)
            {
                result = true;
            }
        }
        return result;
    }
}