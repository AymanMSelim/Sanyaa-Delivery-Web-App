using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IBranchService
    {
        Task<BranchT> GetAsync(int branchId);

        Task<List<BranchT>> GetListAsync();

        Task<int> AddAsync(BranchT branch);

        Task<int> UpdateAsync(BranchT branch);


    }
}
