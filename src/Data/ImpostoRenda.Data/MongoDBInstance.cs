using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ImpostoRenda.Data
{
    public sealed class MongoDBInstance : IMongoDBInstance
    {
        public IMongoDatabase db { get; private set; }

        private string Base { get; set; }

        public MongoDBInstance(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoSettings");
            this.Base = settings["baseName"];
            var client = new MongoClient(settings["connection"]);
            db = client.GetDatabase(Base);
        }

 

        public IMongoDatabase ReturnDB()
        {
            return db;
        }
    }
}
