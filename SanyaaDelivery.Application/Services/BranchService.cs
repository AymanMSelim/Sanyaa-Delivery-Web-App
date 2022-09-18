using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Domain;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IRepository<BranchT> branchRepository;

        public BranchService(IRepository<BranchT> branchRepository)
        {
            this.branchRepository = branchRepository;
        }
        public async Task<int> AddAsync(BranchT branch)
        {
            await branchRepository.AddAsync(branch);
            return await branchRepository.SaveAsync();
        }

        public Task<BranchT> GetAsync(int branchId)
        {
            return branchRepository.GetAsync(branchId);
        }

        public Task<List<BranchT>> GetListAsync()
        {
            return branchRepository.GetListAsync();
        }

        public Task<int> UpdateAsync(BranchT branch)
        {
            branchRepository.Update(branch.BranchId, branch);
            return branchRepository.SaveAsync();
        }
    }
}
