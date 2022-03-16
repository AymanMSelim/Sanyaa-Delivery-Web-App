using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentT>> GetListAsync();
        Task<DepartmentT> GetAsync(int departmentId);
        Task<DepartmentT> GetAsync(string departmentName);
        Task<int> AddAsync(DepartmentT department);
        Task<int> UpdateAsync(DepartmentT department);
        Task<int> DeleteAsync(int departmentId);
        Task<int> DeleteAsync(string departmentName);
    }
}
