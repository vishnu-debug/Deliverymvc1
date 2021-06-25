using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Deliverymvc1.Data;
using Deliverymvc1.Models;
using Microsoft.AspNetCore.Http;

namespace Deliverymvc1.Controllers
{
    public class AdminsController : Controller
    {
        private readonly DeliverymvcContext _context;

        public AdminsController(DeliverymvcContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admin.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AdminID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminID,Name,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminID,Name,Password")] Admin admin)
        {
            if (id != admin.AdminID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.AdminID))
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
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.AdminID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(e => e.AdminID == id);
        }


        public IActionResult home()
        {
            return View();
        }
        public IActionResult mycustomer()
        {
            return View();
        }

        public IActionResult viewcustomer()
        {
            ViewBag.msg = HttpContext.Session.GetString("Name");

            if (ViewBag.msg != null)
            {
                return RedirectToAction("GetAllProducts", "CustomerController1");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        //To View Executive's Data
        public IActionResult ViewExecutive()
        {
            ViewBag.msg = HttpContext.Session.GetString("Name");

            if (ViewBag.msg != null)
            {
                return RedirectToAction("GetAllProducts", "Executive");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        //To View User's Requests
        public IActionResult Viewcustomerrequest()
        {
            ViewBag.msg = HttpContext.Session.GetString("Name");

            if (ViewBag.msg != null)
            {
                return RedirectToAction("GetAllProducts", "Userreq");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }

        //To View Executive's Data
        public IActionResult ViewExecutiveResponse()
        {
            ViewBag.msg = HttpContext.Session.GetString("Name");
            if (ViewBag.msg != null)
            {
                return RedirectToAction("GetAllProducts", "Executiveres");
            }
            else
            {
                return RedirectToAction("login", "Login");
            }
        }
    }
}
