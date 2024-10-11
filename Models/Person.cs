using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models
{
    public class Person
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")] 
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")] 
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Invalid phone number. The phone number must be in the format (123) 456-7890 or 123-456-7890.")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)] // Date picker will be displayed in the browser
        public DateOnly? DateOfBirth { get; set; }
    }
}
