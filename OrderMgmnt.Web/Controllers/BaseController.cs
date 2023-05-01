using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderMgmnt.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly OrderMgmntContext _dbContext;

        public BaseController(OrderMgmntContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
