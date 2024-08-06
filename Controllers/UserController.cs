using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;
using ProjectCarRental.Models.Interfaces;

namespace ProjectCarRental.Controllers
{
    public class UserController : Controller
    {

        private readonly ICarRegisteration _carRegisteration;

        private readonly IRepository<Bookingform> _repBook;
        public UserController(ICarRegisteration carRegisteration, IRepository<Bookingform> repBook)
        {
            _carRegisteration = carRegisteration;
            _repBook = repBook;
        }

        public IActionResult Booked()
        {
            return View();
        }
        public IActionResult Available()
        {


            return View(_carRegisteration.GetAll());
        }
        [Authorize]
        public IActionResult Bookingform()
        {
            string vcar = Request.Query["car"];
            ViewBag.car = vcar;

            string vcolor = Request.Query["color"];
            ViewBag.color = vcolor;

            string vplate = Request.Query["plate"];
            ViewBag.plate = vplate;

            _carRegisteration.Updatestatus(vcar);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Bookingform(Bookingform cr)
        {
            // Remove the ModelState errors for Email and Status before checking if ModelState is valid
            ModelState.Remove(nameof(cr.Email));
            ModelState.Remove(nameof(cr.Status));

            if (ModelState.IsValid)
            {
                // Set the Status programmatically
                cr.Status = "Pending";
                cr.Email = User.Identity?.Name;

                try
                {
                    _repBook.Add(cr);
                    return Json(new { success = true, message = "Data successfully saved." });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = "An error occurred while saving data." });
                }
            }
            else
            {
                // Log the ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return Json(new { success = false, message = "Invalid data", errors = errors });
            }
        }



        [Authorize]
        public IActionResult Removed(int id)
        {

            _repBook.Delete(_repBook.Get(id));
            return RedirectToAction("ViewBookings", "Home");
        }

        [Authorize]
        public IActionResult UpdateBooking(int id)
        {

            //IRepository<Bookingform> repo = new GenericRepository<Bookingform>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");

            return View("UpdateBooking", _repBook.Get(id));
        }
     

        [HttpPost]
        public IActionResult UpdateBooking(Bookingform obj)
        {
            ModelState.Remove(nameof(obj.Cnic));
              ModelState.Remove(nameof(obj.Status));
            if (ModelState.IsValid)
            {
                obj.Status = "Pending";
               
                _repBook.Update(obj);
                return Json(new { success = true , message="Data Suceesfully ." });
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return Json(new { success = false, message = "Invalid data", errors = errors });



        }

        [HttpPost]
        public IActionResult Removed(Bookingform car)
        {

            //  IRepository<Bookingform> repo = new GenericRepository<Bookingform>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            _repBook.Delete(car);
            return RedirectToAction("ViewBookings", "Home");
        }

    }
}
