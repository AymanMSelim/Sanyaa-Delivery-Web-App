using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class DayWorkingTimeRepository : BaseRepository<DayWorkingTimeT>
    {
        public DayWorkingTimeRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
