using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class DepartmentSub1Repository : BaseRepository<DepartmentSub1T>
    {
        public DepartmentSub1Repository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
