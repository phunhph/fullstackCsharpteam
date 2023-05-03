using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using fullstackCsharp.Models;
using fullstackCsharp.DAO;

public class LoginDAO
{
   
   
    public bool ValidateUser(Login Login)
    {
        bool result = false;
        using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
        {
            // Truy vấn tài khoản
            string query = "SELECT UserName, PassWord, id_r,FullName,Users.Id_u FROM Users join UserRole  on Users.id_u = UserRole.id_u WHERE UserName=@username AND PassWord=@password";
            SqlCommand command = new SqlCommand(query, connection);
            // Truyền tham số vào truy vấn và tự động xác định kiểu dữ liệu
            command.Parameters.AddWithValue("@username", Login.user);
            command.Parameters.AddWithValue("@password", Login.password);
            connection.Open();
            // Thực hiện truy vấn và đọc dữ liệu trả về
            using (SqlDataReader reader = command.ExecuteReader())
            {
                //đọc dữ liệu
                if (reader.Read())
                {
                    // gán dữ liệu vào thuộc tính bên modles
                    Login.user = reader["UserName"].ToString();
                    Login.password = reader["PassWord"].ToString();
                    Login.name = reader["FullName"].ToString();
                    Login.id_nv = reader["Id_u"].ToString();
                    Login.rank = Convert.ToInt32(reader["id_r"]);
                    result = true;
                }
            }
        }
        return result;
    }
}
