using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Models.VendorAddresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorAddressesController : BaseController
    {
        public VendorAddressesController(OrderMgmntContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        [Route("{vendorId}")]
        public async Task<IActionResult> GetVendorAddresses(Guid vendorId)
        {
            var vendor = await _dbContext.Vendors.AsNoTracking().Include(x => x.Addresses).FirstAsync(v => v.Id == vendorId);
            var addresses = vendor.Addresses.Where(a => !a.IsRemoved).ToList();
            return Ok(addresses);
        }

        [HttpPost]
        [Route("{vendorId}")]
        public async Task<IActionResult> CreateVendorAddress(Guid vendorId, CreateVendorAddressRequestDTO dto)
        {
            var vendor = await _dbContext.Vendors
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == vendorId);

            var uniqueId = Guid.NewGuid();
            var address = new VendorAddress
            {
                Id = uniqueId,
                District = dto.District,
                AddressInfo = dto.AddressInfo
            };
            await _dbContext.VendorAddresses.AddAsync(address);
            vendor.Addresses.Add(address);
            await _dbContext.SaveChangesAsync();

            return Ok(uniqueId);
        }

        [HttpPut]
        [Route("{vendorId}")]
        public async Task<IActionResult> UpdateVendorAddress(Guid vendorId, UpdateVendorAddressRequestDTO dto)
        {
            var vendor = await _dbContext.Vendors
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == vendorId);

            var address = vendor.Addresses.First(x => x.Id == dto.Id);
            address.District = dto.District;
            address.AddressInfo = dto.AddressInfo;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{vendorId}")]
        public async Task<IActionResult> DeleteVendorAddress(Guid vendorId, DeleteVendorAddressRequestDTO dto)
        {
            var vendor = await _dbContext.Vendors
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == vendorId);

            var address = vendor.Addresses.First(x => x.Id == dto.Id);
            address.IsRemoved = true;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
