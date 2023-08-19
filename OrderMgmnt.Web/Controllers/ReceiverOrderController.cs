using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.Web.Enums;
using OrderMgmnt.Web.Helpers;
using OrderMgmnt.Web.Models.ReceiverFillOrder;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Controllers
{
    public class ReceiverOrderController : Controller
    {
        private readonly OrderMgmntContext _context;
        public ReceiverOrderController(OrderMgmntContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("[controller]/[action]/{orderId}")]
        public async Task<IActionResult> FillOrder(Guid orderId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o=>o.VendorAddress)
                .ThenInclude(a=>a.Vendor)
                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }

            ViewBag.VendorId = order.VendorAddress.Vendor.Id;

            if (order.GetOrderStatus() == OrderStatus.Created)
            {
                var model = new ReceiverFillOrderModel
                {
                    OrderId = order.Id,
                    ProductDescription = order.ProductDescription,
                    OrderCode = order.OrderCode,

                    //In case operator decided to let receiver to update the fields.
                    ReceiverName = order.ClientName,
                    ReceiverPhoneNumber = order.ClientPhoneNumber,
                    ReceiverDistrict = order.ClientDistrict ?? default,
                    ReceiverAddressInfo = order.ClientAddressInfo,
                    ReceiverNotes = order.ClientNotes,

                    
                };

                ViewBag.DistrictOptions = Enum.GetValues(typeof(AdministrativeDistrict))
                    .Cast<AdministrativeDistrict>()
                    .Select(district => new SelectListItem
                    {
                        Value = ((int)district).ToString(),
                        Text = EnumHelper<AdministrativeDistrict>.GetDisplayValue(district)
                    })
                    .ToList();

                return View(model);
            }
            else
            {
                return View("OrderFilled");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FillOrder(ReceiverFillOrderModel model)
        {
            if (ModelState.IsValid)
            {
                var existingOrder = await _context.Orders.FindAsync(model.OrderId);
                if (existingOrder == null)
                {
                    return NotFound("Order not found");
                }

                existingOrder.ClientFillDate = DateTime.Now;
                existingOrder.ClientName = model.ReceiverName;
                existingOrder.ClientDistrict = model.ReceiverDistrict;
                existingOrder.ClientAddressInfo = model.ReceiverAddressInfo;
                existingOrder.ClientPhoneNumber = model.ReceiverPhoneNumber;
                existingOrder.ClientNotes = model.ReceiverNotes;

                await _context.SaveChangesAsync();

                return View("OrderFilled");
            }

            return View(model);
        }
    }
}
