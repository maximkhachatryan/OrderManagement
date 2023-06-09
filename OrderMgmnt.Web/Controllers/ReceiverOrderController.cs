﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.Web.Enums;
using OrderMgmnt.Web.Helpers;
using OrderMgmnt.Web.Models.ReceiverFillOrder;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VenderAddress;

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
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound("Order not found");
            }
            if (order.GetOrderStatus() == OrderStatus.Created)
            {
                var model = new ReceiverFillOrderModel
                {
                    OrderId = order.Id,
                    ProductDescription = order.ProductDescription
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
                return RedirectToAction(nameof(OrderFilled));
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
                existingOrder.ClientNotes = model.Notes;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(OrderFilled));
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult OrderFilled()
        {
            return View();
        }
    }
}
