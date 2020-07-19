using MongoDB.Driver;

namespace ImpostoRenda.Data
{
    public interface IMongoDBInstance
    {
        IMongoDatabase ReturnDB();
    }
}