using ApiLoginMongo.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiLoginMongo.Data
{
    public class MongoContext
    {
        public readonly IMongoDatabase Context;

        public MongoContext(IOptions<StoreDatabaseSettings> storeDatabaseSettings)
        {
            var mongoClient = new MongoClient(storeDatabaseSettings.Value.ConnectionString);
            Context = mongoClient.GetDatabase(storeDatabaseSettings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return Context.GetCollection<User>("Users");
            }
        }
    }
}
