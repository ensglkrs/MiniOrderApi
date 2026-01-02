namespace MiniOrderApi.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = new();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}