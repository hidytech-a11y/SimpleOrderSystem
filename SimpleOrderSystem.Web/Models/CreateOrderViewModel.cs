


namespace SimpleOrderSystem.Web.Models;

public class CreateOrderViewModel
{
    public List<ProductOrderItem> Products { get; set; } = new();
}

public class ProductOrderItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }

}
