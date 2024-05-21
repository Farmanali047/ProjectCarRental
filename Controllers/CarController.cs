using Microsoft.AspNetCore.Mvc;

namespace ProjectCarRental.Controllers
{
    public class CarController : Controller
    {
        public IActionResult TotalCars()
        {
            return View();
        }
        public IActionResult booked()
        {
            return View();
        }
        public IActionResult Available()
        {
            return View();
        }
        public IActionResult Removed()
        {
            return View();
        }

    }
}
