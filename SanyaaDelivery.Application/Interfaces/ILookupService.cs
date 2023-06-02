using SanyaaDelivery.Domain.DTOs;
using SanyaaDelivery.Domain.DTOs.Lookup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface ILookupService
    {
        Task<List<DepartmentLookupDto>> Department();
        Task<List<LookupDto>> Governorate();
        Task<List<LookupDto>> City(int governorateId);
        Task<List<LookupDto>> DepatmentSub0(int departmentId);
        Task<List<LookupDto>> DepatmentSub1(int departmentSub0Id);
        Task<List<LookupDto>> Service(int departmentSub1Id);
    }
}
