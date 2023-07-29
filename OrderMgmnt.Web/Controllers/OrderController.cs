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
        [Route("{vendorId}")]
        public async Task<IActionResult> GetAllOrders(Guid vendorId)
        {
            var vendorExists = await _context.Vendors.AnyAsync(v => v.Id == vendorId);
            if (!vendorExists)
                return BadRequest("Vendor not found!");

            var orders = _context.Vendors
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
                    VendorAddress = new
                    {
                        Id = o.VendorAddress.Id,
                        o.VendorAddress.District,
                        o.VendorAddress.AddressInfo
                    },
                    Status = o.GetOrderStatus()
                })
                .ToList();

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequestDTO request)
        {
            var vendorAddress = await _context.VendorAddresses
                .FirstOrDefaultAsync(a => a.Id == request.VendorAddressId);

            if (vendorAddress == null)
            {
                return BadRequest("VendorAddress not found!");
            }

            //var lastCode = _context.Orders
            //    .Where(o => o.VendorAddress == vendorAddress)
            //    .Select(o => o.OrderCode)
            //    .OrderByDescending(o => o)
            //    .Take(1)
            //    .FirstOrDefault();

            var order = new Order
            {
                Id = Guid.NewGuid(),
                ProductDescription = request.ProductDescription,
                VendorAddress = vendorAddress,
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

        //[HttpPost("/{vendorId}/{orderId}")]
        //public async Task<IActionResult> PlaceOrder(Guid vendorId, Guid orderId, OrderModel orderDetails)
        //{
        //    var order = await _context.Orders.FindAsync(orderId);
        //    if (order == null  || order.VendorId != vendorId)
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
