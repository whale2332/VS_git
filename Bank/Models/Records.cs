using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Bank.Common;

namespace Bank.Models
{
    public class Records
    {
        public string Time { get; set; }
        public string CardId1 { get; set; }
        public string CardId2 { get; set; }
        public double Money { get; set; }


        public static List<Records> GetRecordList(string cardid)//查询转账信息
        {
            DataTable dt = SqlHelper.ExecuteTable("SELECT * FROM Records WHERE CardId1='" + cardid + "' ");
            List<Records> records = new List<Records>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                records.Add(ToModel(dt.Rows[i]));
            }
            return records;
        }


        private static Records ToModel(DataRow dr)
        {
            Records records = new Records();
            records.Time = dr["Time"].ToString();
            records.CardId1 = dr["CardId1"].ToString();
            records.CardId2 = dr["CardId2"].ToString();
            records.Money = (double)dr["Money"];
            return records;
        }
    }
}
