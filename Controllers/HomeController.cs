using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ProjectCarRental.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            {
                string data = String.Empty;
                if (HttpContext.Request.Cookies.ContainsKey("first_request"))
                {
                    string firstVisitedDateTime = HttpContext.Request.Cookies["first_request"];
                    data = "Welcome back " + firstVisitedDateTime;

                }
                else
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = System.DateTime.Now.AddDays(3);
                    data = "you visited firs time";
                    HttpContext.Response.Cookies.Append("first_request", System.DateTime.Now.ToString(), option);
                }
                return View("index", data);
            }
        }
        public IActionResult About()
        {

            HttpContext.Response.Cookies.Delete("first_request");
            return View("Index");
        }
       public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Rental()
        {
            return View();
        }
        public IActionResult Pakage()
        {
            return View();
        }
        public IActionResult Insurance()
        {
            return View();
        }
        public IActionResult Available()
        {
            return View();
        }
        public IActionResult Booked()
        {
            return View();
        }

        [Authorize]
        public IActionResult Bookingform()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Bookingform(string PersonName, string Carname, string cnicNumber, string phoneNumber,
                           string email, string carPlate, string carColor,
                           string pickupLocation, DateTime pickupDate, DateTime returnDate,
                           string carType, string someInformation)
        {
            Bookingform bf = new Bookingform()
            { PersonName = PersonName, CarName = Carname,
                CarPlateNumber = carPlate,
                CarColor = carColor,
                PickupLocation = pickupLocation,
                PickUpDate = pickupDate,
                ReturnDate = returnDate,
                Cnic = cnicNumber,
                PhoneNumber = phoneNumber,
                Email = email,
                CarType = carType,
                SomeInformation = someInformation
            };
            BookingRepository bookingRepository = new BookingRepository();  
            bookingRepository.Add(bf);
            return RedirectToAction("ViewBookings","Home");
        }

        public IActionResult ViewBookings()
        {
            BookingRepository bookingRepository = new BookingRepository();
            List<Bookingform> bookingform = bookingRepository.GetAll();
            return View(bookingform);
        }

        public IActionResult Totalcars()
        {
            CarRegisterationRepository  registerationRepository = new CarRegisterationRepository();

            return View(registerationRepository.GetAll());
        }
      

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
