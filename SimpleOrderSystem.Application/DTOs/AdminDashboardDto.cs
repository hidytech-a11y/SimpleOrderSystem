

namespace SimpleOrderSystem.Application.DTOs
{
    public class AdminDashboardDto
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int PendingOrders { get; set; }  
        public decimal TotalRevenue { get; set; }
    }
}
