namespace Repository.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // Foreign key till Product
        public Product Product { get; set; } // Navigering till Product
        public int OrderId { get; set; } // Foreign key till Order
        public Order Order { get; set; } // Navigering till Order
    }
}
