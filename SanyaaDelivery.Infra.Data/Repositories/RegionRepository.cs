using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class RegionRepository : BaseRepository<RegionT>
    {
        public RegionRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
