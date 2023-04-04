using App.Global;
using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Global.DateTimeHelper;

namespace SanyaaDelivery.Application.Services
{
    public class FawryChargeService : IFawryChargeService
    {
        private readonly IRepository<FawryChargeT> fawryChargeRepository;
        private readonly IRepository<FawryChargeRequestT> fawryChargeRequestRepository;

        public FawryChargeService(IRepository<FawryChargeT> fawryChargeRepository, IRepository<FawryChargeRequestT> fawryChargeRequestRepository)
        {
            this.fawryChargeRepository = fawryChargeRepository;
            this.fawryChargeRequestRepository = fawryChargeRequestRepository;
        }

        public Task<int> AddAsync(FawryChargeT fawryCharge)
        {
            fawryChargeRepository.AddAsync(fawryCharge);
            return fawryChargeRepository.SaveAsync();
        }

        public Task<int> AddChargeRequestAsync(FawryChargeRequestT fawryChargeRequest)
        {
            fawryChargeRequestRepository.AddAsync(fawryChargeRequest);
            return fawryChargeRequestRepository.SaveAsync();
        }

        public Task<FawryChargeT> GetAsync(int id, bool includeRequest = false)
        {
            var query = fawryChargeRepository.Where(d => d.SystemId == id).AsQueryable();
            if (includeRequest)
            {
                query = query.Include(d => d.FawryChargeRequestT);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<bool> IsThisChargeExistAsync(string employeeId, decimal amount)
        {
            return fawryChargeRepository.DbSet.AnyAsync(d => d.EmployeeId == employeeId && d.ChargeAmount == amount
            && d.ChargeStatus != App.Global.Enums.FawryRequestStatus.PAID.ToString()
            && d.ChargeExpireDate < DateTime.Now.EgyptTimeNow());
        }

        public Task<FawryChargeT> GetByReferenceNumberAsync(long referenceNumber, bool includeRequest = false)
        {
            var query = fawryChargeRepository.Where(d => d.FawryRefNumber == referenceNumber).AsQueryable();
            if (includeRequest)
            {
                query = query.Include(d => d.FawryChargeRequestT);
            }
            return query.FirstOrDefaultAsync();
        }

        public Task<int> UpdateAsync(FawryChargeT fawryCharge)
        {
            fawryChargeRepository.Update(fawryCharge.SystemId, fawryCharge);
            return fawryChargeRepository.SaveAsync();
        }

        public async Task<int> UpdateStatus(int id, Enums.FawryRequestStatus status)
        {
            var fawryCharge = await GetAsync(id);
            fawryCharge.ChargeStatus = status.ToString();
            fawryChargeRepository.Update(fawryCharge.SystemId, fawryCharge);
            return await fawryChargeRepository.SaveAsync();
        }
    }
}
