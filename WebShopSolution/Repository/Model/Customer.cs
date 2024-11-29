using System.ComponentModel.DataAnnotations;

namespace Repository.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
