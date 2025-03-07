using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
namespace SportsPro.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
    [Required(ErrorMessage = "Please enter a first name.")]

        public string FirstName { get; set; } = string.Empty;
            [Required(ErrorMessage = "Please enter a last name.")]

		public string LastName { get; set; } = string.Empty;
           [Required(ErrorMessage = "Please enter an address.")]

	    public string Address { get; set; } = string.Empty;
            [Required(ErrorMessage = "Please enter a city.")]

		public string City { get; set; } = string.Empty;
            [Required(ErrorMessage = "Please enter a state.")]

		public string State { get; set; } = string.Empty;
            [Required(ErrorMessage = "Please enter a postal code.")]

		public string PostalCode { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone number must be in (999) 999-9999 format.")]
    [RegularExpression(@"^\(\d{3}\) \d{3}-\d{4}$", ErrorMessage = "Phone number must be in (999) 999-9999 format.")]
 
		public string? Phone { get; set; }
		 [Required(ErrorMessage = "Please enter a valid email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
    [Required(ErrorMessage = "Please select a country.")]

        public string CountryID { get; set; } = string.Empty;  // foreign key property
        
		public Country? Country { get; set; } = null!;          // navigation property

        // A read-only property for display purposes
        public string FullName => FirstName + " " + LastName;

        // Navigation property to Registration (many-to-many)
        // A customer can register many products
        public ICollection<Registration>? Registrations { get; set; }
    }
}
