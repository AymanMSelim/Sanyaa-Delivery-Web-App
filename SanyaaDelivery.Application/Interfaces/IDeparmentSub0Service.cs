using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IDeparmentSub0Service
    {
        Task<DepartmentSub0T> GetAsync(int departmentId);
        Task<List<DepartmentSub0T>> GetListAsync();
        Task<List<DepartmentSub0T>> GetListAsync(string departmentName);
        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(string departmentName);
        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(int departmentId);
    }
}
