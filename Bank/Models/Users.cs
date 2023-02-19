using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bank.Common;

namespace Bank.Models
{
    public class Users
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string MiBaoWT { get; set; }

        public string MiBaoDA { get; set; }

        public static List<Users> GetUserList()//获取全部用户信息
        {
            DataTable dt= SqlHelper.ExecuteTable("SELECT * FROM Users");
            List<Users> users = new List<Users>();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                users.Add(ToModel(dt.Rows[i]));
            }
            return users;
        }

        public static string Rmibao(string phone)//获取密保问题
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE Phone='" + phone + "' ");
            if (dt.Rows.Count == 0)
            {
                return "please input a correct phone number";
            }
            List<Users> users = new List<Users>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                users.Add(ToModel(dt.Rows[i]));
            }
            return users[0].MiBaoWT;
        }
        public static bool IfUser(string phone, string password)//判断账号密码是否正确
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE Phone='" + phone + "' and Password='" + password + "'");
            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool Checkmibao(string answer,string newpassword)//判断密保是否正确
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE MiBaoDA='" + answer + "' ");
            if (dt.Rows.Count != 0)
            {
                SqlHelper.InsertTable("UPDATE Users SET Password=" + newpassword + " WHERE MiBaoDA='" + answer + "'");
                return true;
            }
            else
                return false;
        }

        public static bool Insert(string phone, string name, string password, string mwt, string mda)//插入新用户
        {

            

            SqlHelper.InsertTable("insert into Users (Phone,Name,Password,MiBaoWT,MiBaoDA) values ('" + phone + "','" + name + "','" + password + "','" + mwt + "','" + mda + "')");
      

            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Users WHERE Phone='" + phone + "' and Password='" + password + "'");
            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
                return false;
        }



        private static Users ToModel(DataRow dr)
        {
            Users user = new Users();
            user.Phone = dr["Phone"].ToString();
            user.Name = dr["Name"].ToString();
            user.Password = dr["Password"].ToString();
            user.MiBaoWT = dr["MiBaoWT"].ToString();
            user.MiBaoDA = dr["MiBaoDA"].ToString();
            return user;
        }
    }
}
