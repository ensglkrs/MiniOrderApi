using System.Collections.Generic;

namespace MiniOrderApi.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}