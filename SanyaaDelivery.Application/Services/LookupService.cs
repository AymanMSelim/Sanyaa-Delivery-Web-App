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

        public Task<List<LookupDto>> Governorate()
        {
            return context.GovernorateT.Select(d => new LookupDto
            {
                Id = d.GovernorateId,
                Name = d.GovernorateName
            }).ToListAsync();
        }
    }
}
