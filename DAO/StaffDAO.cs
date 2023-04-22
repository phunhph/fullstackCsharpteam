﻿using fullstackCsharp.Models;
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
                string commandText = "SELECT * FROM NhanVien where status='active' ";
                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;
                // load

                // nếu là select
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id_nv = (string)reader[0];
                        string name = (string)reader[1];
                        string gender = (string)reader[2];
                        string phone = (string)reader[3];
                        string address = (string)reader[4];
                        string username = (string)reader[5];
                        string passwords = (string)reader[6];
                        int rank = (int)reader[7];
                        string status = (string)reader[8];
                        string id_pb = (string)reader[9];
                        string id_rank = (string)reader[10];
                       

                        Staff newStaff = new Staff( id_nv, name, phone,gender, address, username, passwords, rank, status, id_pb, id_rank);
                        staffList.Add(newStaff);
                        // return danh sách nhân viên ở đây
                        //Console.WriteLine(String.Format("{0}", reader[0]));
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

    public bool Delete(String ID_NV)
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
                command.Parameters.AddWithValue("@ID_NV", ID_NV);
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

                command.Parameters.Add("@ID_NV", SqlDbType.VarChar).Value= newStaff.Manv;
                command.Parameters.AddWithValue("@HoTen", newStaff.Namenv);
                command.Parameters.AddWithValue("@Gioitinh", newStaff.Gender);
                command.Parameters.AddWithValue("@SDT", newStaff.Phone);
                command.Parameters.AddWithValue("@Diachi", newStaff.Address);
                command.Parameters.AddWithValue("@username", newStaff.User);
                command.Parameters.AddWithValue("@passwords", newStaff.Password);

                //command.Parameters.Add("@rank",SqlDbType.Int).Value = newStaff.Rank;
                command.Parameters.AddWithValue("@rank", 2);
                // command.Parameters.AddWithValue("@rank", newStaff.Rank + "");

                command.Parameters.AddWithValue("@status", newStaff.Status);
                command.Parameters.AddWithValue("@ID_PB", newStaff.Id_pb);
                command.Parameters.AddWithValue("@ID_Rank", newStaff.Id_rank);


               // command.Parameters.Add("@manv", SqlDbType.VarChar).Value = manv;




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

}

