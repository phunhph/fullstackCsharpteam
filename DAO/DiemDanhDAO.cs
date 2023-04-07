using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace fullstackCsharp.DAO
{
    public class DiemDanhDAO
    {
        static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";
        public bool Commitout(Diemdanh Call, string id_nv)
        {
            bool resultout = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Truy vấn tài khoản
                string query = "SELECT HoTen,r.ID_NV,time_in,time_out FROM NhanVien as nv join roll_call as r on nv.ID_NV =r.ID_NV where r.ID_NV=@id_nv ";
                SqlCommand command = new SqlCommand(query, connection);
                // Truyền tham số vào truy vấn và tự động xác định kiểu dữ liệu
                command.Parameters.AddWithValue("@id_nv",id_nv);
                Console.WriteLine(id_nv);
                connection.Open();
                // Thực hiện truy vấn và đọc dữ liệu trả về
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    //đọc dữ liệu
                    if (reader.Read())
                    {
                        // gán dữ liệu vào thuộc tính bên modles
                        Call.name = reader["HoTen"].ToString();
                        Call.timein = reader["time_in"].ToString();
                        Call.timeout = reader["time_out"].ToString();
                        Call.id_nv = reader["ID_NV"].ToString();
                        resultout = true;
                    }
                }
            }
            return resultout;
        }

        public bool Commitin(Diemdanh Call, string id_nv,string id,string time_in)
        {
            bool resultin = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "INSERT INTO roll_call (ID, ID_NV,time_in) VALUES (@id, @id_nv,@time_in)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@id_nv", id_nv);
                command.Parameters.AddWithValue("@time_in", time_in);
                // Thực hiện truy vấn
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                    return resultin=true;
                }
                else
                {
                    return resultin=false;
                }
            }
        }


    }
}
