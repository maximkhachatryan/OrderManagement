using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Helpers;
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
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderMgmntContext _context;

        public OrderController(ILogger<OrderController> logger, OrderMgmntContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        [HttpGet]
        [Route("{venderId}")]
        public async Task<IActionResult> GetAllOrders(Guid venderId)
        {
            var venderExists = await _context.Venders.AnyAsync(v => v.Id == venderId);
            if (!venderExists)
                return BadRequest("Vender not found!");

            var orders = _context.Venders
                .Include(x => x.Addresses)
                .ThenInclude(x => x.Orders)
                .SelectMany(v => v.Addresses)
                .Where(a => !a.IsRemoved)
                .SelectMany(a => a.Orders)
                .OrderByDescending(x => x.CreateDate)
                .Select(o => new
                {
                    o.ProductDescription,
                    o.CreateDate,
                    o.Id,
                    o.IsDeliveryPaymentByClient,
                    o.OtherNotes,
                    o.DesiredPickUpDate,
                    o.ProductPrice,
                    o.ShouldProductPriceBePaid,
                    VenderAddress = new
                    {
                        Id = o.VenderAddress.Id,
                        o.VenderAddress.District,
                        o.VenderAddress.AddressInfo
                    },
                    Status = o.GetOrderStatus()
                })
                .ToList();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequestDTO request)
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
                DesiredPickUpDate = request.PickUpDate,
                IsDeliveryPaymentByClient = request.IsDeliveryPaymentByClient,
                ShouldProductPriceBePaid = request.ShouldProductPriceBePaid,
                ProductPrice = request.ProductPrice,
                OtherNotes = request.OtherNotes,
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
