using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ImpostoRenda.Domain.Entities
{
    public class EntityBase
    {
        [BsonId]
        public Guid Id { get; protected set; }
    }
}
