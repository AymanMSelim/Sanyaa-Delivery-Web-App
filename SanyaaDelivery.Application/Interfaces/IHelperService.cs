using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IHelperService
    {
        Task<int> GetMinimumCharge(int cityId, int departmentId);
    }
}
