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
                string commandText = "SELECT * FROM NhanVien";
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
                        //int SDT = (int)reader[3];
                        string Diachi = (string)reader[4];
                        string username = (string)reader[5];
                        string passwords = (string)reader[6];

                        Staff newStaff = new Staff(ID_NV, HoTen, Gioitinh, Diachi, username, passwords);
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
                string commandText = "INSERT INTO [dbo].[NhanVien] ([ID_NV],[HoTen],[Gioitinh],[SDT],[Diachi],[username],[passwords],[rank],[ID_PB],[ID_Rank]) VALUES (@ID_NV,@HoTen,@Gioitinh,@SDT,@Diachi,@username,@passwords,@rank,@ID_PB,@ID_Rank)";

                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;

                command.Parameters.AddWithValue("@ID_NV", newStaff.Manv);
                command.Parameters.AddWithValue("@HoTen", newStaff.Tennv);
                command.Parameters.AddWithValue("@Gioitinh", newStaff.Sex);
                command.Parameters.AddWithValue("@SDT", 012345);
                command.Parameters.AddWithValue("@Diachi", newStaff.Address);
                command.Parameters.AddWithValue("@username", newStaff.User);
                command.Parameters.AddWithValue("@passwords", newStaff.Password);
                command.Parameters.AddWithValue("@rank", 2);
                command.Parameters.AddWithValue("@ID_PB", "PB1");
                command.Parameters.AddWithValue("@ID_Rank", "R1");
               
             

             

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
    public List<Staff> Creates(Staff newStaff)
    {
        List<Staff> staffList = new List<Staff>();
        using (SqlConnection dbConnection = new SqlConnection(connString))
        {
            dbConnection.Open();
            DbTransaction transaction = dbConnection.BeginTransaction();
            try
            {

                int timeout = 30;
                string commandText = "INSERT INTO [dbo].[NhanVien] ([ID_NV],[HoTen],[Gioitinh],[SDT],[Diachi],[username],[passwords],[rank],[ID_PB],[ID_Rank]) VALUES (@ID_NV,@HoTen,@Gioitinh,@SDT,@Diachi,@username,@passwords,@rank,@ID_PB,@ID_Rank)";

                SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                command.CommandTimeout = timeout;

                command.Parameters.AddWithValue("@ID_NV", newStaff.Manv);
                command.Parameters.AddWithValue("@HoTen", newStaff.Tennv);
                command.Parameters.AddWithValue("@Gioitinh", newStaff.Sex);
                command.Parameters.AddWithValue("@SDT", 012345);
                command.Parameters.AddWithValue("@Diachi", newStaff.Address);
                command.Parameters.AddWithValue("@username", newStaff.User);
                command.Parameters.AddWithValue("@passwords", newStaff.Password);
                command.Parameters.AddWithValue("@rank", 2);
                command.Parameters.AddWithValue("@ID_PB", "PB1");
                command.Parameters.AddWithValue("@ID_Rank", "R1");





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

