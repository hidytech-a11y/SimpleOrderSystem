using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Web.Extensions;

public static class OrderStatusExtensions
{
    public static string ToBadgeClass(this OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Pending => "bg-warning text-dark",
            OrderStatus.Paid => "bg-info",
            OrderStatus.Processing => "bg-primary",
            OrderStatus.Shipped => "bg-secondary",
            OrderStatus.Completed => "bg-success",
            OrderStatus.Cancelled => "bg-danger",
            _ => "bg-light text-dark"
        };
    }
}
