
namespace ProjectCarRental.Models

{
    public class Bookingform
    {
        public string PersonName { get; set; }
        public string CarName { get; set; }
        public string Cnic { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CarPlateNumber { get; set; }
        public string CarColor { get; set; }
        public string PickupLocation { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CarType { get; set; }
        public string SomeInformation { get; set; }

        
    }
}
