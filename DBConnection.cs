using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanhKeo
{
    internal class DBConnection
    {
        private string connectionString = Properties.Settings.Default.connStr;

        // Method to get a connection object - this is what was missing
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Hàm thực thi câu lệnh không trả dữ liệu (Insert, Update, Delete) - KHÔNG có parameter
        public void ThucThi(string SQL)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thực thi thất bại: " + ex.Message);
                }
            }
        }

        // Hàm thực thi câu lệnh có SqlParameter[]
        public void ThucThi(string SQL, SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL, conn);
                    cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Thực thi thất bại (có tham số): " + ex.Message);
                }
            }
        }

        // Trả về một giá trị đơn (Scalar) - KHÔNG có parameter
        public object ThucThiGiaTri(string sql)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thực thi truy vấn giá trị: " + ex.Message);
                }
            }
            return result;
        }

        // Trả về một giá trị đơn (Scalar) - CÓ parameter
        public object ThucThiGiaTri(string sql, SqlParameter[] parameters)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    result = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi thực thi truy vấn giá trị (có tham số): " + ex.Message);
                }
            }
            return result;
        }

        // Trả về DataTable
        public DataTable LayDuLieu(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu: " + ex.Message);
                }
            }
            return dt;
        }

        // Trả về DataTable với parameters
        public DataTable LayDuLieu(string sql, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu (có tham số): " + ex.Message);
                }
            }
            return dt;
        }
    }
}