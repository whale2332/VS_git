using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bank.Common;

namespace Bank.Models
{
    public class Cards
    {
        public string Phone { get; set; }
        public string CardId { get; set; }
        public double Balance { get; set; }

        public string SchoolCId { get; set; }

        public double SchoolBlance { get; set; }

        public static bool IfTransfer(string cardid1, string cardid2, double money)//转账是否成功
        {
            DataTable dt1 = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE CardId='" + cardid1 + "'");
            DataTable dt2 = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE CardId='" + cardid2 + "'");
            if (dt1.Rows.Count == 0|| dt2.Rows.Count == 0)
            {
                return false;
            }
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cards.Add(ToModel(dt1.Rows[i]));
            }
            if(cards[0].Balance<money)
            {
                return false;
            }
            else
            {
                SqlHelper.InsertTable("UPDATE Cards SET Balance=Balance-" + money + " WHERE CardId='" + cardid1 + "'");
                SqlHelper.InsertTable("UPDATE Cards SET Balance=Balance+" + money + " WHERE CardId='" + cardid2 + "'");

                string time= DateTime.Now.ToString();
                double money1 = money * (-1);
                SqlHelper.InsertTable("insert into Records (Time,CardId1,CardId2,Money) values ('" + time + "','" + cardid1 + "','" + cardid2 + "','" + money + "')");
                SqlHelper.InsertTable("insert into Records (Time,CardId1,CardId2,Money) values ('" + time + "','" + cardid2 + "','" + cardid1 + "','" + money1 + "')");
                return true;
            }
                
        }

        public static bool IfPutin(string scardid, double money)//充值是否成功
        {
            DataTable dt1 = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE SchoolCId='" + scardid + "'");
            
            if (dt1.Rows.Count == 0)
            {
                return false;
            }
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cards.Add(ToModel(dt1.Rows[i]));
            }
            if (cards[0].Balance < money)
            {
                return false;
            }
            else
            {
                SqlHelper.InsertTable("UPDATE Cards SET Balance=Balance-" + money + " WHERE SchoolCId='" + scardid + "'");
                SqlHelper.InsertTable("UPDATE Cards SET SchoolBlance=SchoolBlance+" + money + " WHERE SchoolCId='" + scardid + "'");
                return true;
            }

        }

        public static double SearchBalance(string cardid)//查询余额
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE CardId='"+cardid+"' ");
            if (dt.Rows.Count == 0)
            {
                return -1;
            }
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cards.Add(ToModel(dt.Rows[i]));
            }
            return cards[0].Balance;
        }

        public static double SearchSBalance(string cardid)//查询校园卡余额
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE SchoolCId='" + cardid + "' ");
            if (dt.Rows.Count == 0)
            {
                return -1;
            }
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cards.Add(ToModel(dt.Rows[i]));
            }
            return cards[0].SchoolBlance;
        }

        public static string SearchCardId(string scardid)
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Cards WHERE SchoolCId='" + scardid + "' ");
            if (dt.Rows.Count == 0)
            {
                return "can not find the scardid";
            }
            List<Cards> cards = new List<Cards>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cards.Add(ToModel(dt.Rows[i]));
            }
            return cards[0].CardId;
        }


        private static Cards ToModel(DataRow dr)
        {
            Cards card = new Cards();
            card.Phone = dr["Phone"].ToString();
            card.CardId = dr["CardId"].ToString();
            card.Balance = (double)dr["Balance"];
            card.SchoolCId = dr["SchoolCId"].ToString();
            card.SchoolBlance = (double)dr["SchoolBlance"];

            return card;
        }//nihao
    }
}
