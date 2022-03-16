﻿using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class DepartmentRepository : BaseRepository<DepartmentT>
    {
        public DepartmentRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
