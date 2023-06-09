﻿using fullstackCsharp.Models;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System.Data.Common;

namespace fullstackCsharp.DAO
{
    public class FormDAO
    {
        
        // thêm form
        public bool CommitForm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
            {
                // Tạo đối tượng thực thi truy vấn
                string query = "INSERT INTO form (ID,id_u,TimeStart,TineEnd,thongso,TrangThai) VALUES (@id,@id_nv,@start,@end,@thongso,@trangthai)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", form.id);
                command.Parameters.AddWithValue("@id_nv",form.id_nv);
                command.Parameters.AddWithValue("@start",form.start);
                command.Parameters.AddWithValue("@end",form.end);
                command.Parameters.AddWithValue("@thongso", form.tong);
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
        //suất form staff
        public List<Form> Select(string manv)
        {
            List<Form> formList = new List<Form>();
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM form WHERE TrangThai=@TrangThai and id_u=@id_nv";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    // set param
                    command.Parameters.AddWithValue("@id_nv", manv);
                    command.Parameters.AddWithValue("@TrangThai", "Đã gửi");
                    // load

                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Form from = new Form();
                            from.Soform = Convert.ToInt32(reader["Soform"]);
                            from.id = reader["ID"].ToString();
                            from.id_nv = reader["id_u"].ToString();
                            from.start = reader["TimeStart"].ToString();
                            from.end = reader["TineEnd"].ToString();
                            from.TrangThai = reader["TrangThai"].ToString();
                            formList.Add(from);
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

        // check staff

        public bool checkStaff(string manv,Form form)
        {
            bool Check = false;
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT sum(thongso) FROM form where id_u=@id_nv and ID=@id group by id_u";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    command.Parameters.AddWithValue("@id_nv", manv);
                    command.Parameters.AddWithValue("@id", form.id);
                    
                    // Thực hiện truy vấn
                    object result = command.ExecuteScalar();
                    float sumThongSo = Convert.ToSingle(result); // ép kiểu từ object sang float
                    float ts =form.tong;
                    float checkSum= sumThongSo + ts;
                    Check = checkSum <= 1;
                   
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

            return Check;

        }

        public bool checkActive(string manv)
        {
            bool Check = false;
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT count(*) FROM form where TrangThai=@TrangThai and id_u=@id_u";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    command.Parameters.AddWithValue("@TrangThai", "Đã gửi");
                    command.Parameters.AddWithValue("@id_u", manv);
                    // nếu là select
                    // Thực hiện truy vấn
                    object result = command.ExecuteScalar();
                    float sumThongSo = Convert.ToSingle(result); // ép kiểu từ object sang float
                    float checkSum = sumThongSo;
                    Check = checkSum <0;
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

            return Check;

        }

        // xoá form staff
        public bool DeleteForm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
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

        // confirm staff
        public bool Confirm(Form form)
        {
            bool formCheck = false;
            using (SqlConnection connection = new SqlConnection(ConfigSettings.connString))
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

        public List<Form> Selectcf(string manv)
        {
            List<Form> formList = new List<Form>();
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM form where TrangThai=@TrangThai and id_u=@id_nv";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    command.Parameters.AddWithValue("@TrangThai", "Đã duyệt");
                    command.Parameters.AddWithValue("@id_nv", manv);
                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Form from = new Form();
                            from.Soform = Convert.ToInt32(reader["Soform"]);
                            from.id = reader["ID"].ToString();
                            from.id_nv = reader["id_u"].ToString();
                            from.start = reader["TimeStart"].ToString();
                            from.end = reader["TineEnd"].ToString();
                            from.tong = Convert.ToUInt64( reader["thongso"]);
                            from.TrangThai = reader["TrangThai"].ToString();
                            formList.Add(from);
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

        // select all admin
        public List<Form> Select()
        {
            List<Form> formList = new List<Form>();
            using (SqlConnection dbConnection = new SqlConnection(ConfigSettings.connString))
            {
                dbConnection.Open();
                DbTransaction transaction = dbConnection.BeginTransaction();
                try
                {
                    int timeout = 30;
                    string commandText = "SELECT * FROM form where TrangThai=@TrangThai";
                    SqlCommand command = new SqlCommand(commandText, (SqlConnection)transaction.Connection, (SqlTransaction)transaction);
                    command.CommandTimeout = timeout;
                    command.Parameters.AddWithValue("@TrangThai", "Đã gửi");
                    // nếu là select
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Form from = new Form();
                            from.Soform = Convert.ToInt32(reader["Soform"]);
                            from.id = reader["ID"].ToString();
                            from.id_nv = reader["id_u"].ToString();
                            from.start = reader["TimeStart"].ToString();
                            from.end = reader["TineEnd"].ToString();
                            from.TrangThai = reader["TrangThai"].ToString();
                            formList.Add(from);
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
    }
}
