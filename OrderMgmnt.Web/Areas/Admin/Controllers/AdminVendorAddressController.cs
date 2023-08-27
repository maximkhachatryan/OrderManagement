using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Areas.Admin.Models;

namespace OrderMgmnt.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminVendorAddressController : Controller
    {
        private readonly OrderMgmntContext _context;

        public AdminVendorAddressController(OrderMgmntContext context)
        {
            _context = context;
        }

        // GET: AdminVendorAddressController/Create
        public async Task<IActionResult> Create(Guid vendorId)
        {
            var vendor = await _context.Vendors.FindAsync(vendorId);
            if (vendor == null)
            {
                return NotFound("Vendor not found");
            }
            ViewBag.VendorId = vendorId;
            return View();
        }

        // POST: AdminVendorAddressController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid vendorId, [Bind("Id,District,AddressInfo")] AdminVendorAddressCreateEditModel vendorAddressDto)
        {
            if (ModelState.IsValid)
            {
                var vendor = await _context.Vendors.FindAsync(vendorId);
                if (vendor == null)
                {
                    return NotFound("Vendor not found");
                }
                var vendorAddressId = Guid.NewGuid();
                var vendorAddress = new VendorAddress
                {
                    Id = vendorAddressId,
                    District = vendorAddressDto.District,
                    AddressInfo = vendorAddressDto.AddressInfo,
                    Vendor = vendor,
                    IsRemoved = false
                };


                await _context.VendorAddresses.AddAsync(vendorAddress);

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "AdminVendor", new { id = vendorId });
            }

            return View(vendorAddressDto);
        }

        // GET: AdminVendorAddressController/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorAddress = await _context.VendorAddresses
                .AsNoTracking()
                .Include(a=>a.Vendor)
                .FirstOrDefaultAsync(a=>a.Id == id);

            if (vendorAddress == null)
            {
                return NotFound();
            }

            ViewBag.VendorId = vendorAddress.Vendor.Id;

            return View(new AdminVendorAddressCreateEditModel
            {
                Id = vendorAddress.Id,
                District = vendorAddress.District,
                AddressInfo = vendorAddress.AddressInfo
            });
        }

        // POST: AdminVendorAddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,District,AddressInfo")] AdminVendorAddressCreateEditModel vendorAddressDto)
        {
            if (id != vendorAddressDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vendorAddress = await _context.VendorAddresses
                    .AsNoTracking()
                    .Include(a=>a.Vendor)
                    .FirstOrDefaultAsync(a=>a.Id == id);

                if (vendorAddress == null)
                {
                    return NotFound("Vendor address not found");
                }

                var vendorId = vendorAddress.Vendor.Id;

                vendorAddress.District = vendorAddressDto.District;
                vendorAddress.AddressInfo = vendorAddressDto.AddressInfo;

                try
                {
                    _context.Update(vendorAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorAddressExists(vendorAddress.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "AdminVendor", new { id = vendorId });
            }
            return View(vendorAddressDto);
        }

        // GET: AdminVendorAddressController/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorAddress = await _context.VendorAddresses
                .AsNoTracking()
                .Include(a=>a.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vendorAddress == null)
            {
                return NotFound();
            }

            ViewBag.VendorId = vendorAddress.Vendor.Id;

            return View(vendorAddress);
        }

        // POST: AdminVendorAddressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var vendorAddress = await _context.VendorAddresses
                    .AsNoTracking()
                    .Include(a => a.Vendor)
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (vendorAddress == null)
                {
                    return NotFound("Vendor address not found");
                }

                var vendorId = vendorAddress.Vendor.Id;

                _context.VendorAddresses.Remove(vendorAddress);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "AdminVendor", new { id = vendorId });
            }
            catch
            {
                return View();
            }
        }

        private bool VendorAddressExists(Guid id)
        {
            return _context.VendorAddresses.Any(e => e.Id == id);
        }
    }
}
