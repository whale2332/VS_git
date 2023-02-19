using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bank.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace Bank.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        [EnableCors("any")]
        /*public List<Records> RecordList(string cardid)//查询转账信息（返回List<Records>数组）
        {
            List<Records> records = Records.GetRecordList(cardid);
             
            //HttpResponseMessage response = new HttpResponseMessage();
            //response.Content = new StringContent(JsonConvert.SerializeObject(records), System.Text.Encoding.UTF8, "application/json");
            //response.StatusCode = (HttpStatusCode)200;
            //return response;
            //return ResultToJson.toJson(records);

            return records;
        }*/
        public string RecordList(string cardid)//查询转账信息（返回Json字符串）
        {
            List<Records> records = Records.GetRecordList(cardid);
            List<Detail> detail = new List<Detail>();
            for(int i=0;i<records.Count();i++)
            {
                Detail x = new Detail();
                x.Time = records[i].Time;
                x.CardId2 = records[i].CardId2;
                if(records[i].Money<0)
                {
                    x.Type = "Income";
                    x.Money = records[i].Money * (-1);
                }
                else
                {
                    x.Type = "Expenditure";
                    x.Money = records[i].Money;
                }
                detail.Add(x);
            }
            return JsonConvert.SerializeObject(detail);
        }

    }
}
