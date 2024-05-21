using ProjectCarRental.Controllers;
using System.IO;

namespace ProjectCarRental.Models
{
    public class CarRegisteration
    {
      
        public string CarName { get; set; }
        public string CarColor { get; set; }
        public string CarModel { get; set; }
        public int RegisterationNumber {  get; set; }
        public int Rental {  get; set; }

        public IFormFile CarImage { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public int Id { get; set; }




    }
}
