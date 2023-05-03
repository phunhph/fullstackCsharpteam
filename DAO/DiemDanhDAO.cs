using fullstackCsharp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace fullstackCsharp.DAO
{
    public class DiemDanhDAO
    {
        

		public bool CommitIn(Diemdanh diemdanh)
		{
			bool resultin = false;
			using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
			{
				// Tạo đối tượng thực thi truy vấn
				string query = "insert into Attendane(id_a,AttendaneDate,Checkin,id_u) values (@id,@id,@time_in, @id_nv)";
				SqlCommand command = new SqlCommand(query, connection);
				command.Parameters.AddWithValue("@id", diemdanh.id);
				command.Parameters.AddWithValue("@id_nv", diemdanh.id_nv);
                command.Parameters.AddWithValue("@time_in", diemdanh.timein );
                // Thực hiện truy vấn
                try
                {
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
                catch (SqlException ex) {
                    resultin = false;
                }

			}
			return resultin;
		}
        public bool CommitOut(Diemdanh diemdanh)
        {
            bool resultin = false;
            using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "UPDATE Attendane  SET Checkout = @time_out WHERE AttendaneDate=@id AND id_u=@id_nv";
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
              using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
              {
                  dbConnection.Open();
                  DbTransaction transaction = dbConnection.BeginTransaction();
                  try
                  {
                      int timeout = 30;
                      string commandText = "SELECT Attendane.*, Users.FullName FROM Attendane join Users  on Users.id_u = Attendane.id_u  WHERE Attendane.id_u=@id_nv";
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
                              staff.id = reader["AttendaneDate"].ToString();
                              staff.id_nv = reader["id_u"].ToString();
                              staff.name = reader["FullName"].ToString();
                              staff.timein = reader["Checkin"].ToString();
                              staff.timeout = reader["Checkout"].ToString();
                              diemdanhList.Add(staff);
                              // return danh sách nhân viên ở đây
                              
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
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                   
                    int timeout = 30;
                    string commandText = "SELECT Attendane.*,Users.FullName FROM Attendane join Users  on Users.id_u = Attendane.id_u where Checkout is null ";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // set param
                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Diemdanh staff = new Diemdanh();
                            staff.id = reader["AttendaneDate"].ToString();
                            staff.id_nv = reader["id_u"].ToString();
                            staff.name = reader["FullName"].ToString();
                            staff.timein = reader["Checkin"].ToString();
                            staff.timeout = reader["Checkout"].ToString();
                            diemdanhList.Add(staff);
                            // return danh sách nhân viên ở đây
                            
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
        // fixCheck
        public bool FixCheck(Diemdanh diemdanh)
        {
            bool resultin = false;
            using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "UPDATE Attendane  SET Checkout = @time_out WHERE AttendaneDate=@id AND id_u=@id_nv";
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
    }
}