using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using ProjectCarRental.Models;
using ProjectCarRental.Models.Interfaces;
using System.Drawing.Text;

namespace ProjectCarRental.Controllers
{
    [Authorize(Policy ="AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IBookingform _bookingform1;
        private readonly ICarRegisteration _carRegisteration1;

        private readonly IRepository<Bookingform> _repBook;

        private readonly IRepository<CarRegisteration> _repCar;

        private readonly IRepository<Insurance> _repInsur;

        private readonly IRepository<Pakage> _repPak;

        private readonly IRepository<AspNetUsers> _repUser;
        public AdminController(ILogger<AdminController> logger, IWebHostEnvironment env, IBookingform bookingform1, ICarRegisteration carRegisteration1, IRepository<Bookingform> repBook, IRepository<CarRegisteration> repCar, IRepository<Insurance> repInsu, IRepository<Pakage> repPak, IRepository<AspNetUsers> repUser)
        {
            _logger = logger;
            _env = env;
            _bookingform1 = bookingform1;
            _carRegisteration1 = carRegisteration1;
            _repBook = repBook;
            _repCar = repCar;
            _repInsur = repInsu;
            _repPak = repPak;
            _repUser = repUser;
        }
        [Route("/Admin/Panel")]
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Delete(int id)
        {

          //  IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            return View(_repCar.Get(id));
        }
        [HttpPost]
        public IActionResult Delete(CarRegisteration car)
        {

            //IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            _repCar.Delete(car);
            return RedirectToAction("AllCars", "Admin");
            //CarRegisteration cr = new CarRegisteration();
            ////cr.GetPath(cr.CarImage);
            //cr.CarName = CarName;
            //cr.CarColor = CarColor;
            //cr.CarModel = CarModel;
            ////cr.CarImage = CarImage;
            //cr.RegisterationNumber = RegisterationNumber;
            //cr.Rental = rental;
            //CarRegisterationRepository crRepository = new CarRegisterationRepository();
            //crRepository.Delete(cr);

            //return View();
        }

        public IActionResult UpdateRental(int id)
        {
           // IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            return View(_repCar.Get(id));
        }
        [HttpPost]
        public IActionResult UpdateRental(CarRegisteration d)
        {
            //IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            _repCar.Update(d);
            return RedirectToAction("AllCars", "Admin");
        }

        public IActionResult ViewBookings()
        {
           // IRepository<AspNetUsers> repo = new GenericRepository<AspNetUsers>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=User;Integrated Security=True;");

            return View(_repUser.Get());
        }
        public IActionResult Available()
        {
             object data = _carRegisteration1.Available();
            return View(data);

            //CarRegisterationRepository carRepository = new CarRegisterationRepository();
            //List<CarRegisteration> carRegisterations = carRepository.Available();
         
            //return View(carRegisterations);
        }
        public IActionResult AllUserRequest()
        {
            return View(_bookingform1.GetAll());
      
        }
        public IActionResult Accept(int id)
        {
           // IRepository<Bookingform> repo = new GenericRepository<Bookingform>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            var booking = _repBook.Get(id);
            if (booking != null)
            {
                booking.Status = "Accepted";  // Update the status
                _repBook.Update(booking);         // Save the updated booking
            }
            return RedirectToAction("AllUserRequest"); // Redirect to the listing action
        }

        public IActionResult Booked()
        {
            //CarRegisterationRepository carRepository = new CarRegisterationRepository();
            //List<CarRegisteration> carRegisterations = carRepository.Booked();

            return View();
        }
        public IActionResult Insurance()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Insurance(Insurance insurance)
        {
            //IRepository<Insurance> repo = new GenericRepository<Insurance>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            _repInsur.Add(insurance);
            return RedirectToAction("Insurance","Home");
        }
        public IActionResult Pakage()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult Pakage(Pakage pakage)
        {
            //IRepository<Pakage> repo = new GenericRepository<Pakage>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            _repPak.Add(pakage);
            return RedirectToAction("Pakage", "Home");
        }
        public IActionResult Total()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AllCars()
        {
            string message = string.Empty;

            if (HttpContext.Request.Cookies.ContainsKey("first-visit"))
            {
                string? data = HttpContext.Request.Cookies["first-visit"];

                message = $"Welcome Back {data}";
            }

            else
            {
                CookieOptions option = new CookieOptions();
                option.Expires = System.DateTime.Now.AddDays(1);

                message = $"Welcome! You visited first time";
                HttpContext.Response.Cookies.Append("first-visit", DateTime.Now.ToString(), option);
            }


            object data1 = _carRegisteration1.GetAll();

            CarData carData = new CarData {
                data = message,
                registrations = (List<CarRegisteration>)data1
            
            };

            return View (carData);
        }
        public IActionResult CarRegisteration()
        {
            return View();
        }
        public IActionResult Search(string personName)
        {
            List<Bookingform> bookingforms = _repBook.Get();
            var filterbooking = bookingforms.Where(p => p.PersonName.Contains(personName, StringComparison.OrdinalIgnoreCase)).ToList();
            return View("_search", filterbooking);
            //return PartialView("_search",_bookingform1.GetByName(personName));
        }

        public string getImageUrl(IFormFile myFile)
        {
            if (myFile != null && myFile.Length > 0)
            {
                string folderPath = Path.Combine("Cars", "Booking"); // Relative path from wwwroot
                string fileName = Path.Combine(folderPath, Guid.NewGuid().ToString() + myFile.FileName);
                string wwwRootPath = _env.WebRootPath;
                if (!Directory.Exists(Path.Combine(wwwRootPath, folderPath))) // Check if path exists relative to wwwroot
                {
                    Directory.CreateDirectory(Path.Combine(wwwRootPath, folderPath)); // Create directory if needed
                }

                string filePath = Path.Combine(wwwRootPath, fileName); // Full path for saving
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    myFile.CopyTo(fileStream);
                }
                return $"\\{fileName}"; // Return file path relative to wwwroot
            }
            return string.Empty;
        }

       
        [HttpPost]
        public IActionResult CarRegisteration(CarRegisteration cr, IFormFile carImage)
        {
            if (carImage != null && carImage.Length > 0)
            {
                cr.ImgUrl = getImageUrl(carImage);
                if (string.IsNullOrEmpty(cr.ImgUrl))
                {
                    return Json(new { success = false, message = "Failed to upload image." });
                }
            }
            else
            {
                return Json(new { success = false, message = "No image file uploaded." });
            }

            // Assuming _repCar is properly initialized and injected
            _repCar.Add(cr);

            return Json(new { success = true, message = "Car Registered Successfully." });
        }
        //public IActionResult CarRegisteration(CarRegisteration cr,IFormFile carImage)
        //{
        //    cr.ImgUrl = getImageUrl(carImage);
        //    //cr.ImageName = pic.FileName;
        //    //cr.CarImage = CarImage;
        //    //CarRegisterationRepository crRepository = new CarRegisterationRepository();
        //    //crRepository.Add(cr);

        //   // IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
        //    _repCar.Add(cr);
        //   return Json(new {success= true,Message="Car Registered Successfuly."});
        //    return Json(new { success = false, message = "No image file uploaded." });
        //}
    }
       

    }

