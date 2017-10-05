using System;
using System.Collections.Generic;
using System.Text;

namespace ImpostoRendaLB3.Domain.Entities
{
   public class EntityBase
    {
        public object id;

        public Guid Id { get; protected set; }
    }
}
