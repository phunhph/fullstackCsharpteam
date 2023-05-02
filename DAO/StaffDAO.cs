using fullstackCsharp.Models;
using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using NuGet.Common;
using NuGet.Protocol.Plugins;

namespace fullstackCsharp.DAO;




public class StaffDAO
{
    static string connString = "Data Source=DESKTOP-HOA8KMR;Initial Catalog=QuanLy1;Integrated Security=True;TrustServerCertificate=true";

    //public List<Staff> Select(string manv)
    //{
    //    List<Staff> staffList = new List<Staff>();
    //    using (SqlConnection dbConnection = new SqlConnection(connString))
    //    {
    //        dbConnection.Open();
    //        DbTransaction transaction = dbConnection.BeginTransaction();
    //        try
    //        {
    //            int timeout = 30;
    //            string commandText = "SELECT * FROM Nhan_Vien WHERE ID_NV=@manv";
    //            SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
    //            command.CommandTimeout = timeout;
    //            // set param
    //            command.Parameters.Add("@manv", SqlDbType.VarChar).Value = manv;
    //            // load

    //            // nếu là select
    //            using (SqlDataReader reader = command.ExecuteReader())
    //            {
    //                while (reader.Read())
    //                {
    //                    staffList.Add(new Staff(reader[0], reader[1], reader[2]));
    //                    // return danh sách nhân viên ở đây
    //                    Console.WriteLine(String.Format("{0}", reader[0]));
    //                }
    //            }
    //            // nếu là insert, update, delete
    //            // command.ExecuteNonQuery(); 

    //            // vì là select nên không thay đổi gì db, sau khi select xong thì rollback cho chắc
    //            transaction.Rollback();
    //            // nếu là insert, update, delete thì dùng: transaction.Commit();

    //        }
    //        catch
    //        {
    //            // Nếu có lỗi xảy ra thì rollback để tránh làm mất dữ liệu DB
    //            transaction.Rollback();
    //            throw;
    //        }
    //    }

    //    return staffList;
    //}
    //==================================================================================Selected===================================================
    public List<Staff> SelectAll()
    {
        List<Staff> staffList = new List<Staff>();
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "SELECT * FROM NhanVien where status = 'active'";
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                // load

                // nếu là select
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ID_NV = (string)reader[0];
                        string HoTen = (string)reader[1];
                        string Gioitinh = (string)reader[2];
                        string SDT = (string)reader[3];
                        string Diachi = (string)reader[4];
                        string username = (string)reader[5];
                        string passwords = (string)reader[6];
                        int rank = (int)reader[7];
                        string status = (string)reader[8];
                        string id_pb = (string)reader[9];
                        string id_rank = (string)reader[10];

                        Staff staff = new Staff();


                        Staff newStaff = new Staff(ID_NV, HoTen, Gioitinh, SDT, Diachi, username, passwords,rank,status,id_pb,id_rank);
                        staffList.Add(newStaff);
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
   

   

    //==================================================================================Create===================================================
    public List<Staff> Create(Staff newStaff)
    {
        List<Staff> staffList = new List<Staff>();
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {

                int timeout = 30;
                string commandText = "INSERT INTO [dbo].[NhanVien] ([ID_NV],[HoTen],[Gioitinh],[SDT],[Diachi],[username],[passwords],[rank],[status],[ID_PB],[ID_Rank]) VALUES (@ID_NV,@HoTen,@Gioitinh,@SDT,@Diachi,@username,@passwords,@rank,@status,@ID_PB,@ID_Rank)";

                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;

                command.Parameters.AddWithValue("@ID_NV", newStaff.Id_nv);
                command.Parameters.AddWithValue("@HoTen", newStaff.Namenv);
                command.Parameters.AddWithValue("@Gioitinh", newStaff.Gender);
                command.Parameters.Add("@SDT", SqlDbType.VarChar).Value = newStaff.Phone;
               
                command.Parameters.AddWithValue("@Diachi", newStaff.Address);
                command.Parameters.AddWithValue("@username", newStaff.User);
                command.Parameters.AddWithValue("@passwords", newStaff.Password);
                command.Parameters.AddWithValue("@rank", newStaff.Rank);
                command.Parameters.AddWithValue("@status", newStaff.Status);
                command.Parameters.AddWithValue("@ID_PB", newStaff.Id_pb);
                command.Parameters.AddWithValue("@ID_Rank", newStaff.Id_rank);
               
             

             

                int insertResult = command.ExecuteNonQuery();
                if (insertResult == 0)
                {

                    Console.WriteLine("fail!");

                }
                else
                {

                    Console.WriteLine("successfull!");
                }


                // vì là select nên không thay đổi gì db, sau khi select xong thì rollback cho chắc
                //transaction.Rollback();
                // nếu là insert, update, delete thì dùng: transaction.Commit();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra thì rollback để tránh làm mất dữ liệu DB
                transaction.Rollback();
                throw;
            }
        }
        return staffList;
    }
    //==================================================================================Deleted===================================================
    public bool Delete(String id)
    {
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "UPDATE [dbo].[NhanVien]" +
                    " SET [status] = 'deleted'" +
                    " WHERE ID_NV = @ID_NV ";


                Console.WriteLine(commandText);
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                command.Parameters.AddWithValue("@ID_NV", id);
                command.ExecuteNonQuery();
                // vì là select nên không thay đổi gì db, sau khi select xong thì rollback cho chắc
                //transaction.Rollback();
                // nếu là insert, update, delete thì dùng: transaction.Commit(); transaction.Rollback();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra thì rollback để tránh làm mất dữ liệu DB
                transaction.Rollback();
                throw;
            }
        }
        return true;
    }

    //================================================================================== Edit ===================================================
    public bool Edit(String id, Staff staff)

    {

        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "UPDATE [dbo].[NhanVien]" +




                    " SET [HoTen] = @HoTen , [SDT]= @SDT" +
                    ",[Gioitinh] = @Gioitinh" +
                    
                    ",[Diachi]= @Diachi" +
                    ",[username]= @username" +
                    ",[passwords]= @passwords" +
                    ",[status] = @status" +
                    ",[rank]= @rank" +
                    ",[ID_PB]= @ID_PB" +
                    ",[ID_Rank]= @ID_Rank" +
                    " WHERE ID_NV = @ID_NV";

               // Staff staff = new Staff();
                Console.WriteLine(commandText);
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                command.Parameters.AddWithValue("@ID_NV", id);
                command.Parameters.AddWithValue("@HoTen", staff.Namenv);
                command.Parameters.AddWithValue("@Gioitinh", staff.Gender);
                command.Parameters.AddWithValue("@SDT", staff.Phone);
                command.Parameters.AddWithValue("@Diachi", staff.Address);
                command.Parameters.AddWithValue("@username", staff.User);
                command.Parameters.AddWithValue("@passwords", staff.Password);
                command.Parameters.AddWithValue("@rank", staff.Rank);
                command.Parameters.AddWithValue("@status", staff.Status);
                command.Parameters.AddWithValue("@ID_PB", staff.Id_pb);
                command.Parameters.AddWithValue("@ID_Rank", staff.Id_rank);


                int insertResult = command.ExecuteNonQuery();
                if (insertResult == 0)
                {

                    Console.WriteLine("fail!");

                }
                else
                {

                    Console.WriteLine("successfull!");
                }
                // vì là select nên không thay đổi gì db, sau khi select xong thì rollback cho chắc
                //transaction.Rollback();
                // nếu là insert, update, delete thì dùng: transaction.Commit(); transaction.Rollback();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Nếu có lỗi xảy ra thì rollback để tránh làm mất dữ liệu DB
                transaction.Rollback();
                throw;
            }
        }
        return true;
    }
    //=================================================================================================================================================

    public Staff SelectById(String id)
    {
        Staff staff = null;
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {
                int timeout = 30;
                string commandText = "SELECT * FROM NhanVien where ID_NV = @ID_NV and status = 'active'";
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                // load
                command.Parameters.AddWithValue("@ID_NV", id);
                // nếu là select
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ID_NV = (string)reader[0];
                        string HoTen = (string)reader[1];
                        string Gioitinh = (string)reader[2];
                        string SDT = (string)reader[3];
                        string Diachi = (string)reader[4];
                        string username = (string)reader[5];
                        string passwords = (string)reader[6];
                        int rank = (int)reader[7];
                        string status = (string)reader[8];
                        string id_pb = (string)reader[9];
                        string id_rank = (string)reader[10];



                        
                        staff = new Staff(ID_NV, HoTen, Gioitinh, SDT, Diachi, username, passwords, rank, status, id_pb, id_rank);
                       // staff.Add(newStaff);
                        // return danh sách nhân viên ở đây
                        Console.WriteLine(String.Format("{0}", reader[0]));
                    }
                }
                // nếu là insert, update, delete
                // command.ExecuteNonQuery(); 
                //hello

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

        return staff;
    }
}

