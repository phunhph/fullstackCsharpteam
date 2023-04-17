using fullstackCsharp.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace fullstackCsharp.DAO
{
    public class FormDAO
    {
        static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";
        // thêm form
        public bool CommitForm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "INSERT INTO form (ID,ID_NV,TimeStart,TineEnd,TrangThai) VALUES (@id,@id_nv,@start,@end,@trangthai)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", form.id);
                command.Parameters.AddWithValue("@id_nv",form.id_nv);
                command.Parameters.AddWithValue("@start",form.start);
                command.Parameters.AddWithValue("@end",form.end);
                command.Parameters.AddWithValue("@trangthai", form.TrangThai);
                // Thực hiện truy vấn
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                    formCheck = true;
                }
                else
                {
                    formCheck = false;
                }
            }
            return formCheck;
        }
        //suất form
        public List<Form> Select(string manv)
        {
            List<Form> formList = new List<Form>();
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM form WHERE ID_NV=@id_nv";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // set param
                    command.Parameters.AddWithValue("@id_nv", manv);
                    // load

                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Form from = new Form();
                            from.Soform = Convert.ToInt32(reader["Soform"]);
                            from.id = reader["ID"].ToString();
                            from.id_nv = reader["ID_NV"].ToString();
                            from.start = reader["TimeStart"].ToString();
                            from.end = reader["TineEnd"].ToString();
                            from.TrangThai = reader["TrangThai"].ToString();
                            formList.Add(from);
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

            return formList;

        }
        // xoá form
        public bool DeleteForm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "delete form where Soform = @Soform ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Soform", form.Soform);
                // Thực hiện truy vấn
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                    formCheck = true;
                }
                else
                {
                    formCheck = false;
                }
            }
            return formCheck;
        }
        // select all
        public List<Form> Select()
        {
            List<Form> formList = new List<Form>();
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM form";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Form from = new Form();
                            from.Soform = Convert.ToInt32(reader["Soform"]);
                            from.id = reader["ID"].ToString();
                            from.id_nv = reader["ID_NV"].ToString();
                            from.start = reader["TimeStart"].ToString();
                            from.end = reader["TineEnd"].ToString();
                            from.TrangThai = reader["TrangThai"].ToString();
                            formList.Add(from);
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

            return formList;

        }
        // confirm
        public bool Confirm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "update form set TrangThai=@TrangThai where Soform = @Soform ";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Soform", form.Soform);
                command.Parameters.AddWithValue("@TrangThai", "Đã duyệt");
                // Thực hiện truy vấn
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                    formCheck = true;
                }
                else
                {
                    formCheck = false;
                }
            }
            return formCheck;
        }
    }
}
