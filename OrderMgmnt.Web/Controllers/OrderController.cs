using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Models;
using OrderMgmnt.Web.Models.Order;
using OrderMgmnt.Web.Models.PlaceOrder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderMgmntContext _context;

        public OrderController(ILogger<OrderController> logger, OrderMgmntContext context)
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

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderRequestDTO request)
        {
            var venderAddress = await _context.VenderAddresses.FindAsync(request.VenderAddressId);
            if (venderAddress == null)
            {
                return BadRequest("VenderAddress not found!");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductDescription = request.ProductDescription,
                VenderAddress = venderAddress,
                PickUpDate = request.PickUpDate,
                IsDeliveryPaymentByClient = request.IsDeliveryPaymentByClient,
                ShouldProductPriceBePaid = request.ShouldProductPriceBePaid,
                ProductPrice = request.ProductPrice,
                CreateDate = DateTime.Now
            };

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();
            return Ok();
        }

        //[HttpPost("/{venderId}/{orderId}")]
        //public async Task<IActionResult> PlaceOrder(Guid venderId, Guid orderId, OrderModel orderDetails)
        //{
        //    var order = await _context.Orders.FindAsync(orderId);
        //    if (order == null  || order.VenderId != venderId)
        //    {
        //        return BadRequest("Order not found!");
        //    }

        //    if (order.ClientFillDate != null)
        //        return BadRequest("Order already is filled!");

        //    order.ClientAddress = orderDetails.DeliverAddress;
        //    order.ClientName = orderDetails.Name;
        //    order.ClientPhoneNumber = orderDetails.PhoneNumber;
        //    order.ClientPreferredDate = orderDetails.PreferedDate;
        //    order.ClientFillDate = DateTime.UtcNow;

        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
