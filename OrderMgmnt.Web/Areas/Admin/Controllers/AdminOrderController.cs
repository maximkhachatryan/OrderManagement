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
using static OrderMgmnt.DAL.Entities.VenderAddress;

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
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.VenderAddress)
                .ThenInclude(a => a.Vender)
                .ToListAsync();
            return View(orders.Select(o => new AdminOrderListItemModel
            {
                Id = o.Id,
                CreationDate = o.CreateDate,
                ProductDescription = o.ProductDescription,
                Sender = o.VenderAddress.Vender.Name
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
                .Include(o => o.VenderAddress)
                .ThenInclude(a => a.Vender)
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
                PickupAddress = $"ք․ Երևան, {EnumHelper<AdministrativeDistrict>.GetDisplayValue(order.VenderAddress.District)}, {order.VenderAddress.AddressInfo}",
                DesiredPickupDate = order.DesiredPickUpDate,
                ProductDescription = order.ProductDescription,
                ProductPrice = order.ProductPrice,
                ReceiverAcceptDate = order.ClientFillDate,
                ReceiverAddress = order.ClientAddress,
                ReceiverChangeDeliveryDate = order.ClientChangeDeliveryDate,
                ReceiverName = order.ClientName,
                ReceiverNotes = order.ClientNotes,
                ReceiverPhoneNumber = order.ClientPhoneNumber,
                ReceiverRejectionDate = order.ClientRejectDate,
                RejectDate = order.RejectDate,
                ReturnBackToVenderDate = order.SentBackToVenderDate,
                Status = EnumHelper<OrderStatus>.GetDisplayValue(order.GetOrderStatus()),
                SuccessfulDeliveryDate = order.DeliveryEndDate,
                Vender = order.VenderAddress.Vender.Name,
                VenderNotes = order.OtherNotes
            });
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductDescription,DesiredPickUpDate,IsDeliveryPaymentByClient,ShouldProductPriceBePaid,ProductPrice,OtherNotes,CreateDate,ClientFillDate,AcceptDate,RejectDate,ActualPickUpDate,DeliveryStartDate,DeliveryEndDate,ClientRejectDate,SentBackToVenderDate,ClientName,ClientPhoneNumber,ClientAddress,ClientNotes")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductDescription,DesiredPickUpDate,IsDeliveryPaymentByClient,ShouldProductPriceBePaid,ProductPrice,OtherNotes,CreateDate,ClientFillDate,AcceptDate,RejectDate,ActualPickUpDate,DeliveryStartDate,DeliveryEndDate,ClientRejectDate,SentBackToVenderDate,ClientName,ClientPhoneNumber,ClientAddress,ClientNotes")] Order order)
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

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
