using System.ComponentModel.DataAnnotations;

namespace Repository.Model
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int? ParentOrderId { get; set; }
        public Order ParentOrder { get; set; }

        public List<Order> SubOrders { get; set; } = new List<Order>();

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

}
