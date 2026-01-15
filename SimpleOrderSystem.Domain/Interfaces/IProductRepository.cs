using SimpleOrderSystem.Domain.Entities;


namespace SimpleOrderSystem.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
