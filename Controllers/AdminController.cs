using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;

namespace ProjectCarRental.Controllers
{
    [Authorize(Policy ="AdminPolicy")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IWebHostEnvironment _env;
        public AdminController(ILogger<AdminController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult DeleteCar()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteCar(string CarName, string CarColor, string CarModel, int RegisterationNumber, int rental)
        {

            CarRegisteration cr = new CarRegisteration();
            //cr.GetPath(cr.CarImage);
            cr.CarName = CarName;
            cr.CarColor = CarColor;
            cr.CarModel = CarModel;
            //cr.CarImage = CarImage;
            cr.RegisterationNumber = RegisterationNumber;
            cr.Rental = rental;
            CarRegisterationRepository crRepository = new CarRegisterationRepository();
            crRepository.Delete(cr);

            return View();
        }

        public IActionResult UpdateRental()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateRental(string CarName, string CarColor, string CarModel, int RegisterationNumber, int rental)
        {

            CarRegisteration cr = new CarRegisteration();
            //cr.GetPath(cr.CarImage);
            cr.CarName=CarName;
            cr.CarColor=CarColor;
            cr.CarModel=CarModel;
            //cr.CarImage = CarImage;
            cr.RegisterationNumber = RegisterationNumber;
            cr.Rental = rental;
            CarRegisterationRepository crRepository = new CarRegisterationRepository();
            crRepository.Update(cr);
            
            return View();
        }

        public IActionResult ViewBookings()
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
        public IActionResult Total()
        {
            return View();
        }
        public IActionResult AllCars()
        {
            CarRegisterationRepository carRegisterationRepository = new CarRegisterationRepository();
            List<CarRegisteration> carRegisteration = carRegisterationRepository.GetAll();
            return View(carRegisteration);
        }
        public IActionResult CarRegisteration()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CarRegisteration(CarRegisteration cr)
        {
            cr.ImagePath = getImageUrl(cr.CarImage);
            //cr.ImageName = pic.FileName;
            //cr.CarImage = CarImage;
            CarRegisterationRepository crRepository = new CarRegisterationRepository();
            crRepository.Add(cr);
            return View();
        }
        //public string GetPath(IFormFile picture)
        //{
        //    string wwwrootPath = _env.WebRootPath;
        //    string path = Path.Combine(Path.Combine(wwwrootPath, "Cars"), "Booking");
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);
        //    if (picture != null && picture.Length > 0)
        //    {
        //        path = Path.Combine(path, picture.FileName);

        //        using (var fileStream = new FileStream(path, FileMode.Create))
        //        {
        //            picture.CopyTo(fileStream);
        //        }
        //    }
        //    return ;
        //}
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

    }
}
