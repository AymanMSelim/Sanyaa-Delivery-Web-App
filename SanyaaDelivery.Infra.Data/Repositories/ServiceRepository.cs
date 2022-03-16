﻿using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class ServiceRepository : BaseRepository<ServiceT>
    {
        public ServiceRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
