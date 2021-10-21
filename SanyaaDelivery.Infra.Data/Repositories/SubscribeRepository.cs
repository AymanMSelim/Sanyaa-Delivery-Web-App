using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class SubscribeRepository : BaseRepository<CleaningSubscribersT>
    {

        public SubscribeRepository(sanyaadatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
