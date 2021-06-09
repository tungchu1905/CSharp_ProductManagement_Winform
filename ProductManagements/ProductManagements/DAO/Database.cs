using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProductManagements.DAO
{
    static class Database // ko can khoi tao doi tuong, goi truc tiep
    {
        internal static SqlConnection getConnection()
        {
            string strCon = ConfigurationManager.ConnectionStrings["managementP"].ToString();
            return new SqlConnection(strCon);
        }

        // select trich xuat du lieus
        internal static DataTable getDataBySQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, getConnection());
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet(); // database Cache
            ds.Clear();
            da.Fill(ds);
            return ds.Tables[0];
        }
        internal static int Execute(string sql, params SqlParameter[] sqlParameter)
        {
            SqlCommand cmd = new SqlCommand(sql, getConnection());
            // truyen mang tham so insert, update, delete
            cmd.Parameters.AddRange(sqlParameter);
            cmd.Connection.Open();
            int result = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return result;
        }
        //public void AddCategory(string sql)
        //{
        //    using (SqlConnection con =  getConnection())
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        //public void UpdateCategory(string sql)
        //{
        //    using (SqlConnection con = getConnection())
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //    }
        //}
        //public void DeleteCategory(string sql)
        //{
        //    using (SqlConnection con = getConnection())
        //    {
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}
