using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BTRS.Controllers
{
    public class BusController : Controller
    {
        private readonly SystemDbContext _context;

        public BusController(SystemDbContext context)
        {
            _context = context;
        }
        // GET: BusController
        public async Task<IActionResult> Index()
        {
            return _context.buses != null ?
                          View(await _context.buses.ToListAsync()) :
                          Problem("Entity set 'SystemDbContext.buses'  is null.");
        }

        // GET: BusController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.buses == null)
            {
                return NotFound();
            }

            var bus = await _context.buses
                .FirstOrDefaultAsync(m => m.BusId == id);
            if (bus == null)
            {
                return NotFound();
            }

            return View(bus);
        }

        // GET: BusController/Create
        public IActionResult Create()
        {
            ViewBag.bus= _context.buses.ToList();
            return View();
        }

        // POST: BusController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bus bus)
        {
            try
            {
                int adminid = (int)HttpContext.Session.GetInt32("adminId");
                Admin admin = _context.admins.Where(
                    a => a.AdminId == adminid
                    ).FirstOrDefault();

                bus.admin = admin;
                _context.buses.Add(bus);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusController/Edit/5
        public ActionResult Edit(int id)
        {
            Bus bus = _context.buses.Where(
                 t => t.BusId == id).FirstOrDefault();
            return View(bus);
        }

        // POST: BusController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Bus bus)
        {
            try
            {
                _context.buses.Update(bus);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BusController/Delete/5
        public ActionResult Delete(int id)
        {
            Bus bus = _context.buses.Where(
                 t => t.BusId == id).FirstOrDefault();
            return View(bus);
        }

        // POST: BusController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Bus buses)
        {
            try
            {
                Bus bus = _context.buses.Find(id);
                if (bus == null)
                {
                    return NotFound();
                }

                _context.buses.Remove(bus);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
