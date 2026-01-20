using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Infrastructure.Data;

public static class ProductSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        if (context.Products.Any())
            return;

        context.Products.AddRange(
            new Product("Laptop", 250000),
            new Product("Phone", 150000),
            new Product("Headset", 35000)
        );

        await context.SaveChangesAsync();
    }
}
