using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Models;
using OrderMgmnt.Web.Models;
using OrderMgmnt.Web.Models.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    public class PlaceOrderController : Controller
    {
        private readonly ILogger<PlaceOrderController> _logger;
        private readonly OrderMgmntContext _context;

        public PlaceOrderController(ILogger<PlaceOrderController> logger, OrderMgmntContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("/{venderId}/{orderId}")]
        public async Task<IActionResult> PlaceOrder(Guid venderId, Guid orderId, OrderModel orderDetails)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null  || order.VenderId != venderId)
            {
                return BadRequest("Order not found!");
            }

            if (order.ClientFillDate != null)
                return BadRequest("Order already is filled!");

            order.ClientAddress = orderDetails.DeliverAddress;
            order.ClientName = orderDetails.Name;
            order.ClientPhoneNumber = orderDetails.PhoneNumber;
            order.ClientPreferredDate = orderDetails.PreferedDate;
            order.ClientFillDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
