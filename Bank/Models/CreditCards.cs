using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bank.Common;

namespace Bank.Models
{
    public class CreditCards
    {
        public static long cnum = 1000000000000000;
        public string Name { get; set; }

        public string IdentityId { get; set; }

        public string CcCardId { get; set; }

        private static CreditCards ToModel(DataRow dr)
        {
            CreditCards creditCard = new CreditCards();
            creditCard.Name = dr["Name"].ToString();
            creditCard.IdentityId = dr["IdentityId"].ToString();
            creditCard.CcCardId = dr["CcCardId"].ToString();
            return creditCard;
        }

        public static bool GenerateCcardId(string name,string identityId)//插入新信用卡
        {
            string ccardid = (cnum++).ToString();
            SqlHelper.InsertTable("insert into CreditCard (Name,IdentityId,CcCardId) values ('" + name + "','" + identityId + "','" + ccardid + "')");
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM CreditCard WHERE Name='" + name + "' and IdentityId='" + identityId + "'");
            if (dt.Rows.Count != 0)
            {
                return true;
            }
            else
                return false;

        }

        public static string SearchCcardId(string identityId)
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM CreditCard WHERE IdentityId='" + identityId + "' ");
            if (dt.Rows.Count == 0)
            {
                return "can not find it";
            }
            List<CreditCards> cards = new List<CreditCards>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cards.Add(ToModel(dt.Rows[i]));
            }
            return cards[0].CcCardId;
        }
    }
}
