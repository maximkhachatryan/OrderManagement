using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMgmnt.DAL;
using OrderMgmnt.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly OrderMgmntContext _dbContext;

        public AccountController(OrderMgmntContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _dbContext.Users
                .Where(u => u.UserName == loginRequest.UserName)
                .AsNoTracking()
                .Include(u=>u.Vender)
                .FirstOrDefaultAsync();
            if (user == null || user.PasswordHash != loginRequest.PasswordHash)
                return BadRequest("Login failed");

            return Ok(user.Vender.Id);
        }
    }
}
