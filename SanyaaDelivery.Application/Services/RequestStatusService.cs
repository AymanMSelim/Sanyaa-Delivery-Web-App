using Microsoft.EntityFrameworkCore;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class RequestStatusService : IRequestStatusService
    {
        private readonly IRepository<RequestStatusT> requestStatusRepository;
        private readonly IRepository<RequestStatusGroupT> requestStatusGroupRepository;

        public RequestStatusService(IRepository<RequestStatusT> requestStatusRepository, IRepository<RequestStatusGroupT> requestStatusGroupRepository)
        {
            this.requestStatusRepository = requestStatusRepository;
            this.requestStatusGroupRepository = requestStatusGroupRepository;
        }
        public Task<List<RequestStatusT>> GetListAsync(int? groupId = null)
        {
            var query = requestStatusRepository.DbSet.AsQueryable();
            if (groupId.HasValue)
            {
                query = query.Where(d => d.RequestStatusGroupId == groupId);
            }
            return query.ToListAsync();
        }

        public Task<List<RequestStatusGroupT>> GetGroupListAsync()
        {
            return requestStatusGroupRepository.GetListAsync();
        }
    }
}
