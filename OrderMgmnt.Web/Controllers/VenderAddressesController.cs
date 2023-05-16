using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.DAL.Entities;
using OrderMgmnt.Web.Models.VenderAddresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenderAddressesController : BaseController
    {
        public VenderAddressesController(OrderMgmntContext dbContext) : base(dbContext)
        {
        }

        [HttpGet]
        [Route("{venderId}")]
        public async Task<IActionResult> GetVenderAddresses(Guid venderId)
        {
            var vender = await _dbContext.Venders.AsNoTracking().Include(x => x.Addresses).FirstAsync(v => v.Id == venderId);
            var addresses = vender.Addresses.Where(a => !a.IsRemoved).ToList();
            return Ok(addresses);
        }

        [HttpPost]
        [Route("{venderId}")]
        public async Task<IActionResult> CreateVenderAddress(Guid venderId, CreateVenderAddressRequestDTO dto)
        {
            var vender = await _dbContext.Venders
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == venderId);

            var uniqueId = Guid.NewGuid();
            var address = new VenderAddress
            {
                Id = uniqueId,
                District = dto.District,
                AddressInfo = dto.AddressInfo
            };
            await _dbContext.VenderAddresses.AddAsync(address);
            vender.Addresses.Add(address);
            await _dbContext.SaveChangesAsync();

            return Ok(uniqueId);
        }

        [HttpPut]
        [Route("{venderId}")]
        public async Task<IActionResult> UpdateVenderAddress(Guid venderId, UpdateVenderAddressRequestDTO dto)
        {
            var vender = await _dbContext.Venders
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == venderId);

            var address = vender.Addresses.First(x => x.Id == dto.Id);
            address.District = dto.District;
            address.AddressInfo = dto.AddressInfo;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Route("{venderId}")]
        public async Task<IActionResult> DeleteVenderAddress(Guid venderId, DeleteVenderAddressRequestDTO dto)
        {
            var vender = await _dbContext.Venders
                .Include(x => x.Addresses)
                .FirstAsync(v => v.Id == venderId);

            var address = vender.Addresses.First(x => x.Id == dto.Id);
            address.IsRemoved = true;

            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
