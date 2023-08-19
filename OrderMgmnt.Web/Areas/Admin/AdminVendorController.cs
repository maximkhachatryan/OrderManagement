using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMgmnt.Web.Areas.Admin
{
    public class AdminVendorController : Controller
    {
        // GET: AdminVendorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminVendorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminVendorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminVendorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminVendorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminVendorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminVendorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminVendorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
