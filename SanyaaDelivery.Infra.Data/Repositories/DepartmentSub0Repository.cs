using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class DepartmentSub0Repository : BaseRepository<DepartmentSub0T>
    {
        public DepartmentSub0Repository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
