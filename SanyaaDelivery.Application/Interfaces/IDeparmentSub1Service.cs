using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    interface IDeparmentSub1Service
    {
        Task<DepartmentSub1T> GetAsync(int departmenSub1tId);

        Task<List<DepartmentSub1T>> GetListAsync();

        Task<List<DepartmentSub1T>> GetListAsync(string departmentName, string departmentSub0);

        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(string departmentName, string departmentSub0);

        Task<List<App.Global.DTOs.ValueWithIdDto>> FilerAsync(int departmentSub0Id);
    }
}
