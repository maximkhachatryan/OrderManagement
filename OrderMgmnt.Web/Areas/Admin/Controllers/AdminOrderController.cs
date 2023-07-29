using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Areas.Admin.Models;
using OrderMgmnt.Web.Enums;
using OrderMgmnt.Web.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using static OrderMgmnt.DAL.Entities.VendorAddress;

namespace OrderMgmnt.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminOrderController : Controller
    {
        private readonly OrderMgmntContext _context;

        public AdminOrderController(OrderMgmntContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminOrders
        public async Task<IActionResult> Index(OrderStatus? status)
        {
            var orders = await _context.Orders
                .Include(o => o.VendorAddress)
                .ThenInclude(a => a.Vendor)
                .OrderBy(o => o.CreateDate)
                .ToListAsync();

            var filteredOrders = orders.Where(o => status == null || o.GetOrderStatus() == status);
            if (status.HasValue)
                ViewBag.Status = EnumHelper<OrderStatus>.GetDisplayValue(status.Value);
            else
                ViewBag.Status = "Բոլորը";

            return View(filteredOrders.Select(o => new AdminOrderListItemModel
            {
                Id = o.Id,
                CreationDate = o.CreateDate,
                ProductDescription = o.ProductDescription,
                Sender = o.VendorAddress.Vendor.Name,
                SenderDistrict = o.VendorAddress.District,
                Receiver = o.ClientName,
                ReceiverDistrict = o.ClientDistrict,
                Status = o.GetOrderStatus()
            }));
        }

        // GET: Admin/AdminOrder/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.VendorAddress)
                .ThenInclude(a => a.Vendor)
                .FirstOrDefaultAsync(m => m.Id == idGuid);
            if (order == null)
            {
                return NotFound();
            }

            return View(new AdminOrderDetailsModel
            {
                AcceptDate = order.AcceptDate,
                ActualPickUpDate = order.ActualPickUpDate,
                DeliveryPrice = order.IsDeliveryPaymentByClient ? 1500.00M : 0,
                DeliveryStartDate = order.DeliveryStartDate,
                OrderCreated = order.CreateDate,
                OrderId = order.Id,
                PickupAddress = $"ք․ Երևան, {EnumHelper<AdministrativeDistrict>.GetDisplayValue(order.VendorAddress.District)}, {order.VendorAddress.AddressInfo}",
                DesiredPickupDate = order.DesiredPickUpDate,
                ProductDescription = order.ProductDescription,
                ProductPrice = order.ProductPrice,
                ReceiverAcceptDate = order.ClientFillDate,
                ReceiverAddress = order.ClientAddressInfo,
                ReceiverChangeDeliveryDate = order.ClientChangeDeliveryDate,
                ReceiverName = order.ClientName,
                ReceiverNotes = order.ClientNotes,
                ReceiverPhoneNumber = order.ClientPhoneNumber,
                ReceiverRejectionDate = order.ClientRejectDate,
                RejectDate = order.RejectDate,
                ReturnBackToVendorDate = order.SentBackToVendorDate,
                Status = order.GetOrderStatus(),
                SuccessfulDeliveryDate = order.DeliveryEndDate,
                Vendor = order.VendorAddress.Vendor.Name,
                VendorNotes = order.OtherNotes
            });
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductDescription,DesiredPickUpDate,IsDeliveryPaymentByClient,ShouldProductPriceBePaid,ProductPrice,OtherNotes,CreateDate,ClientFillDate,AcceptDate,RejectDate,ActualPickUpDate,DeliveryStartDate,DeliveryEndDate,ClientRejectDate,SentBackToVendorDate,ClientName,ClientPhoneNumber,ClientAddressInfo,ClientNotes")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetReceiverData(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }

            order.ClientFillDate = null;
            order.ClientAddressInfo = null;
            order.ClientChangeDeliveryDate = null;
            order.ClientName = null;
            order.ClientNotes = null;
            order.ClientPhoneNumber = null;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnToPreviousState(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }

            var status = order.GetOrderStatus();
            switch (status)
            {
                case OrderStatus.Created:
                case OrderStatus.ClientFilled:
                    //DO NOTHING (no such cases)
                    break;
                case OrderStatus.Accepted:
                    order.AcceptDate = null;
                    break;
                case OrderStatus.Rejected:
                    order.RejectDate = null;
                    break;
                case OrderStatus.PickedUp:
                    order.ActualPickUpDate = null;
                    break;
                case OrderStatus.DeliveryStarted:
                    order.DeliveryStartDate = null;
                    break;
                case OrderStatus.Delivered:
                    order.DeliveryEndDate = null;
                    break;
                case OrderStatus.RejectedByClient:
                    order.ClientRejectDate = null;
                    break;
                case OrderStatus.SentBackToVendor:
                    order.SentBackToVendorDate = null;
                    break;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.RejectDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.AcceptDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReceiveProduct(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.ActualPickUpDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendProduct(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.DeliveryStartDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConsiderDelivered(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.DeliveryEndDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConsiderRejectedByReceiver(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.ClientRejectDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConsiderReturnedBackToVendor(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parsed = Guid.TryParse(id, out var idGuid);
            if (!parsed)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(idGuid);
            if (order == null)
            {
                return NotFound();
            }
            order.SentBackToVendorDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id });
        }


        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
