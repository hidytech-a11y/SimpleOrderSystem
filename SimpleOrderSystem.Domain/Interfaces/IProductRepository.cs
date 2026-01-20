using SimpleOrderSystem.Domain.Entities;


namespace SimpleOrderSystem.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);   //  Guid
        Task AddAsync(Product product);
        void Remove(Product product);
        Task SaveChangesAsync();
    }


}
