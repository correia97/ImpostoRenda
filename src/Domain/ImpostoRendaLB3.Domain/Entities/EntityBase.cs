using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ImpostoRendaLB3.Domain.Entities
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; protected set; }
    }
}
