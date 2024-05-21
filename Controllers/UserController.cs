using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;

namespace ProjectCarRental.Controllers
{
    public class UserController : Controller
    {
        
        
        public IActionResult Booked()
        {
            return View();
        }
        public IActionResult Available()
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
            {
                PersonName = PersonName,
                CarName = Carname,
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
            return RedirectToAction("ViewBookings", "Home");
        }
        [Authorize]
        public IActionResult Removed()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Removed(string PersonName, string Carname, string cnicNumber, string phoneNumber,
                         string email, string carPlate, string carColor,
                         string pickupLocation, DateTime pickupDate, DateTime returnDate,
                         string carType, string someInformation)
        {
            Bookingform bf = new Bookingform()
            {
                PersonName = PersonName,
                CarName = Carname,
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
            bookingRepository.delete(bf);
            return RedirectToAction("ViewBookings", "Home");
        }

    }
}
