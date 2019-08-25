using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImpostoRendaLB3.Domain.Entities
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; protected set; }
    }
}
