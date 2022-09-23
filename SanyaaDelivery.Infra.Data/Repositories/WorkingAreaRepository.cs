using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class WorkingAreaRepository : BaseRepository<WorkingAreaT>
    {
        public WorkingAreaRepository(SanyaaDatabaseContext dbContext, IUnitOfWork unitOfWork) : base(dbContext, unitOfWork)
        {

        }
    }
}
