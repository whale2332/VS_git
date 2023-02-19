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
    public class CardsController : ControllerBase
    {
        [EnableCors("any")]
        public int Transfer(string cardid1,string cardid2,double money)//ת��
        {
            bool iftransfer = Cards.IfTransfer(cardid1, cardid2, money);
            if (iftransfer == true)
            {
                return 1;
            }
            else
                return 0;
        }

        public int BuySchoolBanlance(string scardid, double money)//У԰����ֵ
        {
            bool ifbuy = Cards.IfPutin(scardid, money);
            if (ifbuy == true)
            {
                return 1;
            }
            else
                return 0;
        }

        public string GetID(string scardid)//����У԰��
        {
            string id = Cards.SearchCardId(scardid);
            return id;
        }
        public double GetBalance(string cardid)//��ѯ���
        {
            double bal = Cards.SearchBalance(cardid);
            return bal;
        }

        public double GetSBalance(string cardid)//��ѯУ԰�����
        {
            double bal = Cards.SearchSBalance(cardid);
            return bal;
        }
    }
}
