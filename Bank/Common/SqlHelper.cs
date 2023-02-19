using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Common
{
    public class SqlHelper
    {
        public static string Constr { get; set; }
        public static DataTable ExecuteTable(string cmdText)
        {
            using (SqlConnection con=new SqlConnection(Constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdText,con);//管家
                SqlDataAdapter sda = new SqlDataAdapter(cmd);//推车
                DataSet ds = new DataSet();//卡车
                sda.Fill(ds);
                return ds.Tables[0];
            }
        }
        public static void InsertTable(string cmdText)//插入新用户
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(cmdText, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
            }
        }
    }
}
