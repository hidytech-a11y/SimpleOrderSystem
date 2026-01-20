
using SimpleOrderSystem.Application.Interfaces;
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Interfaces;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Product?> GetByIdAsync(Guid id)
    => await _repository.GetByIdAsync(id);

    public async Task CreateAsync(string name, decimal price)
    {
        var product = new Product(name, price);
        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(Guid id, string name, decimal price)
    {
        var product = await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Product not found");

        product.Update(name, price);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _repository.GetByIdAsync(id)
            ?? throw new InvalidOperationException("Product not found");

        _repository.Remove(product);
        await _repository.SaveChangesAsync();
    }
}
