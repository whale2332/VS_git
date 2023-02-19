using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;

namespace Bank.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [EnableCors("any")]
        public List<Users> GetUsers()//获取全部用户信息
        {
            List<Users> userList= Users.GetUserList();
            return userList;
        }

        public string Getmibao(string phone)//获取密保
        {
            string mibao = Users.Rmibao(phone);
            return mibao;
        }

        public int ChangePassword(string answer, string newpassword)//修改密码
        {
            bool mibao = Users.Checkmibao(answer,newpassword);
            if (mibao)
                return 1;
            else
                return 0;
        }
        public int Login(string phone,string password)//登录
        {
            bool ifuser = Users.IfUser(phone, password);
            if (ifuser == true)
            {
                return 1;
            }
            else
                return 0;

        }

        public int Register(string phone,string name,string password, string mwt, string mda)//注册
        {
            bool ifinsert = Users.Insert(phone, name, password, mwt, mda);
            if (ifinsert == true)
            {
                return 1;
            }
            else
                return 0;
        }

    }
}
