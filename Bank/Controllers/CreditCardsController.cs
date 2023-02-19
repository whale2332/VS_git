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
    public class CreditCardsController : ControllerBase
    {
        [EnableCors("any")]

        public int RegisterCcard(string name, string identityId)//信用卡激活
        {
            bool ifinsert = CreditCards.GenerateCcardId(name, identityId);
            if (ifinsert == true)
            {
                return 1;
            }
            else
                return 0;
        }

        public string FindCardId(string identityId)//查找
        {
            string r = CreditCards.SearchCcardId(identityId);
            return r;
        }

    }
}
