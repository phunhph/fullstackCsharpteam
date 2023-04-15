using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace fullstackCsharp.DAO
{
    public class DiemDanhDAO
    {
        static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";

		public bool CommitIn(Diemdanh diemdanh)
		{
			bool resultin = false;
			using (SqlConnection connection = new SqlConnection(connString))
			{
				// Tạo đối tượng thực thi truy vấn
				string query = "INSERT INTO roll_call (ID, ID_NV,HoTen,time_in) VALUES (@id, @id_nv,@name,@time_in)";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@id", diemdanh.id);
				command.Parameters.AddWithValue("@id_nv", diemdanh.id_nv);
                command.Parameters.AddWithValue("@name", diemdanh.name);
                command.Parameters.AddWithValue("@time_in", diemdanh.timein );
				// Thực hiện truy vấn
				connection.Open();
				int result = command.ExecuteNonQuery();
				connection.Close();

				// Trả về kết quả
				if (result > 0)
				{
					resultin = true;
				}
				else
				{
					resultin = false;
				}
			}
			return resultin;
		}
        public bool CommitOut(Diemdanh diemdanh)
        {
            bool resultin = false;
            using (SqlConnection connection = new SqlConnection(connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "UPDATE roll_call  SET time_out = @time_out WHERE ID=@id AND ID_NV=@id_nv";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", diemdanh.id);
                command.Parameters.AddWithValue("@id_nv", diemdanh.id_nv);
                command.Parameters.AddWithValue("@time_out", diemdanh.timeout);
                // Thực hiện truy vấn
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();

                // Trả về kết quả
                if (result > 0)
                {
                    resultin = true;
                }
                else
                {
                    resultin = false;
                }
            }
            return resultin;
        }
        // thong tin diem danh 

          public List<Diemdanh> Select(string manv)
          {
              List<Diemdanh> diemdanhList = new List<Diemdanh>();
              using (SqlConnection dbConnection = new SqlConnection(connString))
              {
                  dbConnection.Open();
                  DbTransaction transaction = dbConnection.BeginTransaction();
                  try
                  {
                      int timeout = 30;
                      string commandText = "SELECT * FROM roll_call WHERE ID_NV=@id_nv";
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
                              Diemdanh staff = new Diemdanh();
                              staff.id = reader["ID"].ToString();
                              staff.id_nv = reader["ID_NV"].ToString();
                              staff.name = reader["HoTen"].ToString();
                              staff.timein = reader["time_in"].ToString();
                              staff.timeout = reader["time_out"].ToString();
                              diemdanhList.Add(staff);
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

              return diemdanhList;
        
          }
        // select all
        public List<Diemdanh> Select()
        {
            List<Diemdanh> diemdanhList = new List<Diemdanh>();
            using (SqlConnection dbConnection = new SqlConnection(connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                   
                    int timeout = 30;
                    string commandText = "SELECT * FROM roll_call ";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // set param
                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Diemdanh staff = new Diemdanh();
                            staff.id = reader["ID"].ToString();
                            staff.id_nv = reader["ID_NV"].ToString();
                            staff.name = reader["HoTen"].ToString();
                            staff.timein = reader["time_in"].ToString();
                            staff.timeout = reader["time_out"].ToString();
                            diemdanhList.Add(staff);
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

            return diemdanhList;

        }
    }
}