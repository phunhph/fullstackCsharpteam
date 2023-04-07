using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace fullstackCsharp.DAO
{
    public class DiemDanhDAO
    {
        static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";
        public bool Commitout(Diemdanh Call, string id_nv,string time_out,string id)
        {
            bool resultout = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Truy vấn tài khoản
                string query = "update roll_call set time_out=@time_out where ID_NV=@id_nv and ID=@id ";
                SqlCommand command = new SqlCommand(query, connection);
                // Truyền tham số vào truy vấn và tự động xác định kiểu dữ liệu
                command.Parameters.AddWithValue("@id_nv",id_nv);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@time_out", time_out);
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                   resultout = true;
                }
                else
                {
                    resultout = false;
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
                     resultin=true;
                }
                else
                {
                     resultin=false;
                }
            }
            return resultin;
        }
    }
}
// lổi khi vào trang điểm danh thì đồng thời thực hiện cả 2 lệnh insert into và update 
// khắc phục nó bằng việc thực hiện event button điểm danh