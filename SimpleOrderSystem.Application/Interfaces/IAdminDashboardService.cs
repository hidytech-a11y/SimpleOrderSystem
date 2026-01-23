using SimpleOrderSystem.Application.DTOs;

namespace SimpleOrderSystem.Application.Interfaces;

public interface IAdminDashboardService
{
    Task<AdminDashboardDto> GetDashboardDataAsync();
}
