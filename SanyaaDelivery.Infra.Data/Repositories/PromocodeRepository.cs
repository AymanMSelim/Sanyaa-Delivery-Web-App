using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class PromocodeRepository : BaseRepository<PromocodeT>
    {
        public PromocodeRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
