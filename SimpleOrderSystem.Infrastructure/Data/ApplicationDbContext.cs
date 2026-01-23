using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleOrderSystem.Domain.Entities;

namespace SimpleOrderSystem.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderStatusAudit> OrderStatusAudits => Set<OrderStatusAudit>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* =====================================================
         * GUID PRIMARY KEYS (DOMAIN-GENERATED)
         * ===================================================== */
        builder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Entity<Order>()
            .Property(o => o.Id)
            .ValueGeneratedNever();

        builder.Entity<OrderStatusAudit>()
            .Property(a => a.Id)
            .ValueGeneratedNever();

        /* =====================================================
         * PRODUCT
         * ===================================================== */
        builder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        /* =====================================================
         * ORDER
         * ===================================================== */
        builder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        builder.Entity<Order>()
            .Property(o => o.Status)
            .IsRequired();

        /* =====================================================
         * ORDER → ORDER ITEMS (BACKING FIELD)
         * ===================================================== */
        builder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey("OrderId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .Navigation(o => o.OrderItems)
            .UsePropertyAccessMode(PropertyAccessMode.Field);


        /* =====================================================
         * ORDER → STATUS AUDITS (BACKING FIELD)
         * ===================================================== */
        builder.Entity<Order>()
            .HasMany(o => o.StatusAudits)
            .WithOne()
            .HasForeignKey(a => a.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Order>()
            .Navigation(o => o.StatusAudits)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        /* =====================================================
         * ORDER ITEM
         * ===================================================== */
        builder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        /* =====================================================
         * ORDER STATUS AUDIT
         * ===================================================== */
        builder.Entity<OrderStatusAudit>()
            .Property(a => a.ChangedBy)
            .HasMaxLength(256)
            .IsRequired();

        builder.Entity<OrderStatusAudit>()
            .Property(a => a.OldStatus)
            .IsRequired();

        builder.Entity<OrderStatusAudit>()
            .Property(a => a.NewStatus)
            .IsRequired();

        builder.Entity<OrderStatusAudit>()
            .Property(a => a.ChangedAt)
            .IsRequired();
    }
}
