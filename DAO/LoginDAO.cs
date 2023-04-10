using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;
using fullstackCsharp.Models;


namespace fullstackCsharp.DAO;
public class LoginDAO
{

    public bool ValidateUser(Login Login)
    {
        bool result = false;
        using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
        {
            // Truy vấn tài khoản
            string query = "SELECT username, passwords, rank, HoTen,ID_NV FROM NhanVien WHERE username=@username AND passwords=@password";
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
                    Login.user = reader["username"].ToString();
                    Login.password = reader["passwords"].ToString();
                    Login.name = reader["HoTen"].ToString();
                    Login.id_nv = reader["ID_NV"].ToString();
                    Login.rank = Convert.ToInt32(reader["rank"]);
                    result = true;
                }
            }
        }
        return result;
    }
}

