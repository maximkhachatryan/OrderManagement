﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Areas.Admin.Controllers
{
    public class AdminPartnerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
