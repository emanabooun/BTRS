using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace BTRS.Controllers
{
    public class TripController : Controller
    {
        private SystemDbContext _context;
        public TripController(SystemDbContext context)
        {
            _context = context;
        }
        // GET: TripController
        public async Task<IActionResult> Index()
        {
            return _context.trips!= null ?
                          View(await _context.trips.ToListAsync()) :
                          Problem("Entity set 'SystemDbContext.trips'  is null.");
        }


        // GET: TripController/Details/5
        public ActionResult Details(int id)
        {
            Trip trip= _context.trips.Where(
                t => t.TripId == id).FirstOrDefault();
            return View(trip);
        }

        // GET: TripController/Create
        public ActionResult Create()
        {
            ViewBag.Bus = _context.buses.ToList();
            return View();
        }

        // POST: TripController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            string busNumber = form["BusNumber"].ToString();
             
            string Destination = form["Destination"].ToString();
            DateTime startDate = DateTime.Parse(form["StartDate"]);
            DateTime endDate = DateTime.Parse(form["EndDate"]);
            
            int adminid = (int)HttpContext.Session.GetInt32("adminId");
            Admin admin = _context.admins.Where(
                a => a.AdminId == adminid
                ).FirstOrDefault();


            Trip trip = new Trip();
            trip.BusNumber = busNumber;

            trip.Destination = Destination;
            trip.StartDate = startDate;
            trip.EndDate = endDate;
           
            trip.admin = admin;

            _context.trips.Add(trip);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: TripController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Bus = await _context.buses.ToListAsync();

            if (id == null || _context.trips == null)
            {
                return NotFound();
            }

            var trip = await _context.trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // POST: TripController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Trip trip)
        {
            try
            {
                _context.trips.Update(trip);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TripController/Delete/5
        public ActionResult Delete(int id)
        {
            Trip trips = _context.trips.Where(
                t => t.TripId == id).FirstOrDefault();
            return View(trips);
        }

        // POST: TripController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Trip trips)
        {
            try
            {
                Trip trip = _context.trips.Find(id);
                if (trip == null)
                {
                    return NotFound();
                }

                _context.trips.Remove(trip);
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
