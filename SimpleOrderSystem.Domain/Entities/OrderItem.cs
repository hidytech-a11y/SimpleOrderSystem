

namespace SimpleOrderSystem.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; private set; }

        private OrderItem() { } // For EF Core

        public OrderItem(Guid productId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");
           
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public decimal GetTotal() => Quantity * UnitPrice;


    }
}
