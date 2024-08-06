using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectCarRental.Models
{
    public class Bookingform
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "The name can only contain letters and spaces.")]
        public string PersonName { get; set; }

        [Required(ErrorMessage = "Please enter the car name.")]
        public string CarName { get; set; }

        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNIC must be exactly 13 digits long.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNIC must be exactly 13 digits without Dashes")]
        public string Cnic { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } // This will be set from the User.Identity

        [Required(ErrorMessage = "Please enter the car plate number.")]
        public string CarPlateNumber { get; set; }

        [Required(ErrorMessage = "Please enter the car color.")]
        public string CarColor { get; set; }
        [Required(ErrorMessage = "Please enter the pickup location.")]
        [RegularExpression(@"^[^<>@|]+$", ErrorMessage = "The pickup location contains not allowed characters: <, >, @, |.")]
        public string PickupLocation { get; set; }


        [Required(ErrorMessage = "Please enter the pickup date.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time.")]
        public DateTime PickUpDate { get; set; }

        [Required(ErrorMessage = "Please enter the return date.")]
        [DataType(DataType.DateTime, ErrorMessage = "Please enter a valid date and time.")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Please enter the quantity.")]
        public int Quantity { get; set; }

        [DataType(DataType.MultilineText)]
        [RegularExpression(@"^[^<>@|]+$", ErrorMessage = "The Additional Information  contains not allowed characters: <, >, @, |.")]

        public string SomeInformation { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}
