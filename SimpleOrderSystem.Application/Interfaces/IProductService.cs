using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
