using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImpostoRendaLB3.Data
{
    public sealed class MongoDBInstance : IMongoDBInstance
    {
        private string connectionString;
        public IMongoDatabase db { get; private set; }

        private string Host { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string Base { get; set; }

        public MongoDBInstance(IConfiguration configuration)
        {
            var settings = configuration.GetSection("MongoSettings");
            Host = settings["host"];
            Base = settings["baseName"];
            UserName = settings["userName"];
            Password = settings["password"];
            connectionString = $"mongodb://{UserName}:{Password}@{Host}/{Base}";
            var client = new MongoClient(connectionString);
            db = client.GetDatabase(Base);
        }

 

        public IMongoDatabase ReturnDB()
        {
            return db;
        }
    }
}
