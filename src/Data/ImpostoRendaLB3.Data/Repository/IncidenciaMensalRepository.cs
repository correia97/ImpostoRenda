using ImpostoRendaLB3.Domain.Entities;
using ImpostoRendaLB3.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImpostoRendaLB3.Data.Repository
{
    public class IncidenciaMensalRepository : RepositoryBase<IncidenciaMensal>, IIncidenciaMensalRepository
    {
        public IncidenciaMensalRepository(IMongoDBInstance mongoDBInstance)
            : base(mongoDBInstance)
        {

        }
    }
}
