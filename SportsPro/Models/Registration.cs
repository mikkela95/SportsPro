namespace SportsPro.Models
{
    public class Registration
    {
        public int RegistrationID { get; set; }

        // Foreign Keys
        public int CustomerID { get; set; }
        public int ProductID { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public Product? Product { get; set; }
    }
}
