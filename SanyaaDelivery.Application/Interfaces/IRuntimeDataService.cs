using SanyaaDelivery.Domain.OtherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SanyaaDelivery.Application.Interfaces
{
    public interface IRuntimeDataService
    {
        Task<RuntimeData> Get();
        Task<object> Get1();
    }
}
