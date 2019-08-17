using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ImpostoRendaLB3.Data
{
    public sealed class MongoDBInstance : IMongoDBInstance
    {
        private string connectionString;
        public IMongoDatabase db { get; private set; }

        private string Base { get; set; }

        public MongoDBInstance(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoSettings");
            connectionString = settings["connection"];
            this.Base = settings["baseName"];
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(Base);
        }

 

        public IMongoDatabase ReturnDB()
        {
            return db;
        }
    }
}
