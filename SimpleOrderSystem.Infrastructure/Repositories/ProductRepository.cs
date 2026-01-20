using Microsoft.EntityFrameworkCore;
using SimpleOrderSystem.Domain.Entities;
using SimpleOrderSystem.Domain.Interfaces;
using SimpleOrderSystem.Infrastructure.Data;

namespace SimpleOrderSystem.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(Guid id)
        => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AddAsync(Product product)
        => await _context.Products.AddAsync(product);

    public void Remove(Product product)
        => _context.Products.Remove(product);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}

