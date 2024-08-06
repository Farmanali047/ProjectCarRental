using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Authorization;
using ProjectCarRental.Models.Interfaces;
using static Dapper.SqlMapper;

namespace ProjectCarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IBookingform _bookingform1;
        private readonly ICarRegisteration _carRegisteration;
        private readonly IRepository<Insurance> _repositoryin;
        private readonly IRepository<Pakage> _repopak;
        private readonly IRepository<Bookingform> _repBook;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env, IBookingform bookingform1, ICarRegisteration carRegisteration,
            IRepository<Insurance> repositoryinsur, IRepository<Pakage> repopak, IRepository<Bookingform> repBook)
        {
            _logger = logger;
            _env = env;
            _bookingform1 = bookingform1;
            _carRegisteration = carRegisteration;
            _repositoryin = repositoryinsur;
            _repopak = repopak;
            _repBook = repBook;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index action started.");
            Debug.WriteLine("in the index");
            Trace.WriteLine("the indec miefj");
            try
            {
                if (User.Identity.Name == "Admin@gmail.com")
                {
                    _logger.LogInformation("Admin user logged in.");
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    string data = String.Empty;
                    if (HttpContext.Request.Cookies.ContainsKey("first_request"))
                    {
                        string firstVisitedDateTime = HttpContext.Request.Cookies["first_request"];
                        data = "Welcome back " + firstVisitedDateTime;
                        _logger.LogInformation($"Returning user. First visit: {firstVisitedDateTime}");
                    }
                    else
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = System.DateTime.Now.AddDays(3);
                        data = "You visited first time";
                        HttpContext.Response.Cookies.Append("first_request", System.DateTime.Now.ToString(), option);
                        _logger.LogInformation("First-time visitor. Cookie set.");
                    }

                    return View("index", data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index action.");
                return View("Error");
            }
            finally
            {
                _logger.LogInformation("Index action ended.");
            }
        }

        public IActionResult About()
        {
            _logger.LogInformation("About action started.");
            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Contact action started.");
            return View();
        }

        public IActionResult Rental()
        {
            _logger.LogInformation("Rental action started.");
            return View();
        }

        public IActionResult Pakage()
        {
            _logger.LogInformation("Pakage action started.");
            try
            {
                List<Pakage> list = _repopak.Get();
                _logger.LogInformation("Pakage data retrieved.");
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Pakage action.");
                return View("Error");
            }
        }

        public IActionResult Insurance()
        {
            _logger.LogInformation("Insurance action started.");
            try
            {
                List<Insurance> list = _repositoryin.Get();
                _logger.LogInformation("Insurance data retrieved.");
                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Insurance action.");
                return View("Error");
            }
        }

        public IActionResult Available()
        {
            _logger.LogInformation("Available action started.");
            try
            {
                object data = _carRegisteration.GetAll();
                _logger.LogInformation("Available cars data retrieved.");
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Available action.");
                return View("Error");
            }
        }

        public IActionResult Booked()
        {
            _logger.LogInformation("Booked action started.");
            return View();
        }

        [Authorize]
        public IActionResult Bookingform()
        {
            _logger.LogInformation("Bookingform action started.");
            return View();
        }

        [HttpPost]
        public IActionResult Bookingform(Bookingform cr)
        {
            _logger.LogInformation("Bookingform POST action started.");
            try
            {
                _repBook.Add(cr);
                _logger.LogInformation("Booking form submitted.");
                return View(cr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Bookingform POST action.");
                return View("Error");
            }
        }

        [Authorize]
        public IActionResult Removed(int id)
        {
            _logger.LogInformation($"Removed action started for booking ID: {id}.");
            try
            {
                var booking = _repBook.Get(id);
                _logger.LogInformation($"Booking data retrieved for ID: {id}.");
                return View(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in Removed action for ID: {id}.");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Removed(Bookingform car)
        {
            _logger.LogInformation($"Removed POST action started for booking ID: {car.Id}.");
            try
            {
                _repBook.Delete(car);
                _logger.LogInformation($"Booking removed for ID: {car.Id}.");
                return RedirectToAction("ViewBookings", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in Removed POST action for ID: {car.Id}.");
                return View("Error");
            }
        }

        public IActionResult ViewBookings()
        {
            _logger.LogInformation("ViewBookings action started.");
            try
            {
                object data = _bookingform1.GetAll1(User.Identity?.Name);
                _logger.LogInformation("Bookings data retrieved.");
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ViewBookings action.");
                return View("Error");
            }
                }

        public IActionResult Totalcars()
        {
            _logger.LogInformation("Totalcars action started.");
            try
            {
                object data = _carRegisteration.GetAll();
                _logger.LogInformation("Total cars data retrieved.");
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Totalcars action.");
                return View("Error");
            }
        }

        public IActionResult Admin()
        {
            _logger.LogInformation("Admin action started.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogInformation("Error action started.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
