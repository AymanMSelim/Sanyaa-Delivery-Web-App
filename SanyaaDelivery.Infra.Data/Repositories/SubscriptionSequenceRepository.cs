using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class SubscriptionSequenceRepository : BaseRepository<SubscriptionSequenceT>
    {
        public SubscriptionSequenceRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
