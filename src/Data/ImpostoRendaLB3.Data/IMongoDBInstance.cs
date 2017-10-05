using MongoDB.Driver;

namespace ImpostoRendaLB3.Data
{
    public interface IMongoDBInstance
    {
        IMongoDatabase ReturnDB();
    }
}