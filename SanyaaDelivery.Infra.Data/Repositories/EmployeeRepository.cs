using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanyaaDelivery.Infra.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<EmployeeT>
    {
        public EmployeeRepository(SanyaaDatabaseContext dbContext) : base(dbContext)
        {

        }
    }
}
