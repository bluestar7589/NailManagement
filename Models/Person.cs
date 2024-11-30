using System.ComponentModel.DataAnnotations;

namespace NailManagement.Models
{
    /// <summary>
    /// Represents a person with personal information such as name, email, phone number, and date of birth.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// The first name cannot exceed 50 characters in length.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// The last name cannot exceed 50 characters in length.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person.
        /// The email address must be in a valid format.
        /// </summary>
        /// <remarks>
        /// This field is required.
        /// </remarks>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// The phone number must match a specific format (e.g., (123) 456-7890 or 123-456-7890).
        /// </summary>
        /// <remarks>
        /// This field is optional.
        /// </remarks>
        [Display(Name = "Phone Number")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
            ErrorMessage = "Invalid phone number. The phone number must be in the format (123) 456-7890 or 123-456-7890.")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the person.
        /// The date of birth is displayed using a date picker in the browser.
        /// </summary>
        /// <remarks>
        /// This field is optional.
        /// </remarks>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)] // Date picker will be displayed in the browser
        public DateOnly? DateOfBirth { get; set; }
    }
}
