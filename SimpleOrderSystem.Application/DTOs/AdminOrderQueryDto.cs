using SimpleOrderSystem.Domain.Enums;

namespace SimpleOrderSystem.Application.DTOs;

public class AdminOrderQueryDto
{
    public string? SearchTerm { get; set; }
    public OrderStatus? Status { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
