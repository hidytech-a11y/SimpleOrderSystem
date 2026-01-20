

namespace SimpleOrderSystem.Application.DTOs
{
    public class CreateOrderDto
    {
        public Dictionary<Guid, int> ProductQuantities { get; set; } = new();
    }
}
