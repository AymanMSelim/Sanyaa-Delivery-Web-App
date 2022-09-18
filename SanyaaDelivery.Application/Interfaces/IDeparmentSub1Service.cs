using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IDeparmentSub1Service
    {
        Task<int> AddAsync(DepartmentSub1T departmentSub1);

        Task<int> UpdateAsync(DepartmentSub1T departmentSub1);

        Task<DepartmentSub1T> GetAsync(int departmenSub1tId);

        Task<List<DepartmentSub1T>> GetListAsync();

        Task<List<DepartmentSub1T>> GetListAsync(string departmentName, string departmentSub0);
        Task<List<DepartmentSub1T>> GetListAsync(int? mainDepartmentId, int? departmentSub0Id, string departmentSub1Name);

        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(string departmentName, string departmentSub0);

        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(int departmentSub0Id);
        Task<bool> IsExistAsync(string departmentName);

    }
}
