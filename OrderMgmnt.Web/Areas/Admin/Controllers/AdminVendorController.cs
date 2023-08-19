using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
    public class AdminVendorController : Controller
    {
        private readonly OrderMgmntContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminVendorController(OrderMgmntContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            this._env = env;
        }

        // GET: Admin/Vendors
        public async Task<IActionResult> Index()
        {
            var vendors = await _context.Vendors.AsNoTracking()
                .OrderBy(x => x.Code)
                .ToListAsync();

            return View(vendors.Select(v => new AdminVendorListItemModel
            {
                Id = v.Id,
                Code = v.Code,
                Name = v.Name,
                BrandName = v.BrandName,
                VendorWalletAmount = v.VendorWalletAmount
            }));
        }

        // GET: Admin/Vendors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Admin/Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Vendors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,BrandName,Name,PhoneNumber1,PhoneNumber2,InstagramLink,WebsiteLink")] AdminVendorCreateModel vendorDto, [FromForm] IFormFile logoFile)
        {
            if (ModelState.IsValid)
            {
                var vendorId = Guid.NewGuid();
                var vendor = new Vendor
                {
                    Id = vendorId,
                    Code = vendorDto.Code,
                    Name = vendorDto.Name,
                    BrandName = vendorDto.BrandName,
                    PhoneNumber1 = vendorDto.PhoneNumber1,
                    PhoneNumber2 = vendorDto.PhoneNumber2,
                    InstagramLink = vendorDto.InstagramLink,
                    WebsiteLink = vendorDto.WebsiteLink
                };

                var uploaded = await UploadLogo(logoFile, vendorId.ToString());
                if (uploaded)
                {
                    await _context.Vendors.AddAsync(vendor);

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(vendorDto);
        }

        // GET: Admin/Vendors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        // POST: Admin/Vendors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Code,BrandName,Name,PhoneNumber1,PhoneNumber2,InstagramLink,WebsiteLink,VendorWalletAmount")] Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.Id))
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
            return View(vendor);
        }

        // GET: Admin/Vendors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Admin/Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(Guid id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }

        private async Task<bool> UploadLogo(IFormFile logoFile, string filename)
        {
            if (logoFile != null && logoFile.Length > 0 && IsPng(logoFile.FileName))
            {
                // Get the path to the "Images" directory
                var imagesPath = Path.Combine(_env.ContentRootPath, "Images");

                var fullFileName = $"{filename}.png";

                
                // Combine the path with the unique file name
                var filePath = Path.Combine(imagesPath, fullFileName);


                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                // Save the image to the file system
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logoFile.CopyToAsync(stream);
                }

                // You can save 'filePath' in your database to later retrieve the image.
                // Return success or do any other necessary operations.
                return true;
            }
            else
            {
                // Handle the case where no file was uploaded.
                return false;
            }
        }

        private bool IsPng(string fileName)
        {
            if (fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
