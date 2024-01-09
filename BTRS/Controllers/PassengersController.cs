using BTRS.Data;
using BTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BTRS.Controllers
{
    public class PassengersController : Controller
    {
        private SystemDbContext _context;

        public PassengersController(SystemDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(Passengers passengers)
        {
            bool empty = checkEmpty(passengers);

            if (empty == true)
            {
                if (!Checkemail(passengers))
                {
                    if (!Checkphone(passengers))
                    {
                        if (!Checkusername(passengers))
                        {

                            _context.Add(passengers);
                            _context.SaveChanges();
                            TempData["msg"] = "the registration is done";
                        }
                        else
                        {
                            TempData["msg"] = "Username already exists.";
                        }
                    }
                    else
                    {
                        TempData["msg"] = "phone number already exists.";
                    }

                }
                else
                {
                    TempData["msg"] = "email already exists.";
                } }


            else
            {
                TempData["msg"] = "chack data";
            }
            
            return View();
        }
        public bool checkEmpty(Passengers passengers)
        {
            if (String.IsNullOrEmpty(passengers.Name))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(passengers.Email))
            {
                return false;
            }
            else if (passengers.PhoneNumber.ToString().Length == 0)
            {
                return false;
            }
            else if (String.IsNullOrEmpty(passengers.Gender))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(passengers.Username))
            {
                return false;
            }
            else if (String.IsNullOrEmpty(passengers.Password))
            {
                return false;
            }
            
            else
                return true;
        }


        private bool Checkusername(Passengers passengers)
        {
            if (_context.passengers.Any(p => p.Username == passengers.Username))
            {
                ModelState.AddModelError("Username", "Username already exists.");
                return true;
            }
            return false;
        }
        private bool Checkemail(Passengers passengers)
        {
            if (_context.passengers.Any(p => p.Email == passengers.Email))
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return true;
            }
            return false;
        }
        private bool Checkphone(Passengers passengers)
        {
            if (_context.passengers.Any(p => p.PhoneNumber == passengers.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                return true;
            }
            return false;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(Login userlogin)
        {
            if (ModelState.IsValid)
            {
                Passengers? passengers = _context.passengers.Where(u => u.Username.Equals(userlogin.username)
                &&
                u.Password.Equals(userlogin.Password)).FirstOrDefault();

                Admin? admin = _context.admins.Where(a => a.Username.Equals(userlogin.username)
                &&
                a.Password.Equals(userlogin.Password)).FirstOrDefault();

                if (passengers != null)
                {
                    HttpContext.Session.SetInt32("passengersId", passengers.PassengerId);
                    return RedirectToAction("TripList");
                }
                else if (admin != null)
                {
                    HttpContext.Session.SetInt32("adminId", admin.AdminId);
                    return RedirectToAction("Create", "Trip");
                }
                else
                {
                    TempData["Msg"] = "User not found";
                }
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult TripList()
        {
            int passengersid = (int)HttpContext.Session.GetInt32("passengersId");
            List<int> lst_passengers_Trips = _context.passengers_Trips.Where(
                p => p.passengers.PassengerId == passengersid).Select(t => t.trip.TripId).ToList();

            List<Trip> lst_Trip = _context.trips.Where(
                t => lst_passengers_Trips.Contains(t.TripId) == false).ToList();

            return View(lst_Trip);
        }

        public IActionResult Booking(int id)
        {
            int passengersid = (int)HttpContext.Session.GetInt32("passengersId");

            Passengers_Trip passengers_Trip = new Passengers_Trip();
            passengers_Trip.passengers = _context.passengers.Find(passengersid);
            passengers_Trip.trip = _context.trips.Find(id);

            _context.passengers_Trips.Add(passengers_Trip);
            _context.SaveChanges();

            return RedirectToAction("bookingList");
        }

        public IActionResult bookingList()
        {
            int passengersid = (int)HttpContext.Session.GetInt32("passengersId");

            List<int> lst_passengers = _context.passengers_Trips.Where(
               t => t.passengers.PassengerId == passengersid
                ).Select(s => s.trip.TripId).ToList();

            List<Trip> lst_trip = _context.trips.Where(
                t => lst_passengers.Contains(t.TripId)
                ).ToList();

            return View(lst_trip);
        }

        public ActionResult cancel(int id)
        {
            int passengersid = (int)HttpContext.Session.GetInt32("passengersId");

            var passengersTrips = _context.passengers_Trips
                .FirstOrDefault(pt => pt.passengers.PassengerId == passengersid && pt.trip.TripId == id);

            if (passengersTrips != null)
            {
                _context.passengers_Trips.Remove(passengersTrips);
                _context.SaveChanges();
            }

            return RedirectToAction("bookingList");

        }

    }
}