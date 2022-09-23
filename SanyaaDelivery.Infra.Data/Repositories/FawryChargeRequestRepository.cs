using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class FawryChargeRequestRepository : BaseRepository<FawryChargeRequestT>
    {
        public FawryChargeRequestRepository(SanyaaDatabaseContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
