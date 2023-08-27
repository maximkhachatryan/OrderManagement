using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OrderMgmnt.Web.Areas.Admin.Models;

namespace OrderMgmnt.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Display the login form
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        // Process the login form
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AdminLoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Username == _configuration["Authentication:AdminUsername"] &&
                    model.Password == _configuration["Authentication:AdminPassword"])
                {
                    // Create a claims identity for the user
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, _configuration["Authentication:AdminUsername"])
                    // You can add additional claims here if needed
                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Create the authentication properties
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe // Set as needed
                    };

                    // Sign in the user
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    // Authentication succeeded; redirect to a protected area
                    return RedirectToAction("Index", "AdminOrder"); // Replace with your protected area's action and controller
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            return View(model);
        }

        // Add a logout action if needed
    }

}
