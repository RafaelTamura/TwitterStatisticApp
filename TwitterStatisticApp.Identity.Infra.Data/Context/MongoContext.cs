using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace TwitterStatisticApp.Identity.Infra.Data.Context
{
    public class MongoContext
    {
        public IMongoClient dbClient;

        public MongoContext(IConfiguration config)
        {
            dbClient = new MongoClient(string.Format("mongodb://{0}", config["MongoConnectionString"]));
        }
    }
}
