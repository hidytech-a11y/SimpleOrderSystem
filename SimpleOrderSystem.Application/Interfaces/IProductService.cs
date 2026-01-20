using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task CreateAsync(string name, decimal price);
        Task UpdateAsync(Guid id, string name, decimal price);
        Task DeleteAsync(Guid id);
    }
}
