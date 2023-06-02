using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain.DTOs.Lookup;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class LookupService : ILookupService
    {
        private readonly SanyaaDatabaseContext context;

        public LookupService(SanyaaDatabaseContext context)
        {
            this.context = context;
        }
        public Task<List<LookupDto>> City(int governorateId)
        {
            return context.CityT.Where(d => d.GovernorateId == governorateId).Select(d => new LookupDto
            {
                Id = d.CityId,
                Name = d.CityName
            }).ToListAsync();
        }

        public Task<List<DepartmentLookupDto>> Department()
        {
            return context.DepartmentT.Select(d => new DepartmentLookupDto
            {
                Id = d.DepartmentId,
                Name = d.DepartmentName,
                Image = d.DepartmentImage
            }).ToListAsync();
        }

        public Task<List<LookupDto>> DepatmentSub0(int departmentId)
        {
            return context.DepartmentSub0T.Where(d => d.DepartmentId == departmentId).Select(d => new LookupDto
            {
                Id = d.DepartmentSub0Id,
                Name = d.DepartmentSub0
            }).ToListAsync();
        }

        public Task<List<LookupDto>> DepatmentSub1(int departmentSub0Id)
        {
            return context.DepartmentSub1T.Where(d => d.DepartmentSub0Id == departmentSub0Id).Select(d => new LookupDto
            {
                Id = d.DepartmentId,
                Name = d.DepartmentSub1
            }).ToListAsync();
        }

        public Task<List<LookupDto>> Governorate()
        {
            return context.GovernorateT.Select(d => new LookupDto
            {
                Id = d.GovernorateId,
                Name = d.GovernorateName
            }).ToListAsync();
        }

        public Task<List<LookupDto>> Service(int departmentSub1Id)
        {
            return context.ServiceT.Where(d => d.DepartmentId == departmentSub1Id).Select(d => new LookupDto
            {
                Id = d.ServiceId,
                Name = d.ServiceName
            }).ToListAsync();
        }
    }
}
