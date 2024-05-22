using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCarRental.Models;
using ProjectCarRental.Models.Interfaces;

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
        public IActionResult Delete(int id)
        {

            IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            return View(repo.Get(id));
        }
        [HttpPost]
        public IActionResult Delete(CarRegisteration car)
        {

            IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            repo.Delete(car);
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
            IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            return View(repo.Get(id));
        }
        [HttpPost]
        public IActionResult UpdateRental(CarRegisteration d)
        {
            IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            repo.Update(d);
            return RedirectToAction("AllCars", "Admin");
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
        public IActionResult CarRegisteration(CarRegisteration cr,IFormFile carImage)
        {
            cr.ImgUrl = getImageUrl(carImage);
            //cr.ImageName = pic.FileName;
            //cr.CarImage = CarImage;
            //CarRegisterationRepository crRepository = new CarRegisterationRepository();
            //crRepository.Add(cr);

            IRepository<CarRegisteration> repo = new GenericRepository<CarRegisteration>("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=booking;Integrated Security=True;");
            repo.Add(cr);
           // repo.Delete(cr);
            return View();
        }
       

    }
}
