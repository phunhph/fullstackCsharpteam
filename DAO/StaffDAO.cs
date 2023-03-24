using fullstackCsharp.Models;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;

namespace fullstackCsharp.DAO;

public class StaffDAO
{
    static string connString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=QuanLy;Integrated Security=True;TrustServerCertificate=True";
    public List<Staff> Select(string manv)
    {
        List<Staff> staffList = new List<Staff>();
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "SELECT * FROM Nhan_Vien WHERE ID_NV=@manv";
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
                        staffList.Add(new Staff(reader[0], reader[1], reader[2]));
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

        return staffList;
    }
}