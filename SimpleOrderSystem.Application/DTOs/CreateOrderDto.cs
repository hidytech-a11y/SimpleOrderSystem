

namespace SimpleOrderSystem.Application.DTOs
{
    public class CreateOrderDto
    {
        public Dictionary<int, int> ProductQuantities { get; set; } = new();
    }
}
