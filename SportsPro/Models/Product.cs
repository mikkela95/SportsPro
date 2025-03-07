using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string ProductCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

  [Range(typeof(decimal), "0.01", "79228162514264337593543950335", 
            ErrorMessage = "Yearly price must be greater than zero.")]
        [Column(TypeName = "decimal(8,2)")]

        // [Column(TypeName = "decimal(8,2)")]
		
        public decimal YearlyPrice { get; set; }

        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        // Navigation property to Registration (many-to-many)
        // A product can be registered by many customers
        public ICollection<Registration>? Registrations { get; set; }
    }
}
