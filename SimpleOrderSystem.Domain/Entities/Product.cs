

namespace SimpleOrderSystem.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        private Product() { } // For EF Core


        public Product(string name, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name is required");

            if (price <= 0)
                throw new ArgumentException("Price ,ust be greater than zero");

            Name = name;
            Price = price;
        }
    }
}
