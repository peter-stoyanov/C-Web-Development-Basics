using System.Collections.Generic;

namespace ShopHierarchy.Models
{
    public class Order
    {
        public Order()
        {
            this.Items = new List<ItemOrder>();
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<ItemOrder> Items { get; set; }
    }
}