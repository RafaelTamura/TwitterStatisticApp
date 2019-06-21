using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TwitterStatisticApp.Infra.Data.Context;

namespace TwitterStatisticApp.Infra.Data.Repository
{
    public class RepositoryBase<TEntity> : IDisposable
    {
        private IMongoDatabase database;

        private JsonSerializerSettings settings = new JsonSerializerSettings();

        public RepositoryBase(IConfiguration config)
        {
            database = new MongoContext(config).dbClient.GetDatabase(config["DatabaseName"]);

            settings.TypeNameHandling = TypeNameHandling.Auto;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public void Add(TEntity obj)
        {
            database.GetCollection<TEntity>(typeof(TEntity).Name.ToString()).InsertOne(obj);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name.ToString()).Find(filter).ToEnumerable();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return database.GetCollection<TEntity>(typeof(TEntity).Name.ToString()).Find(x => true).ToEnumerable();
        }

        public void Update(TEntity obj, Expression<Func<TEntity, bool>> filter)
        {
            database.GetCollection<TEntity>(typeof(TEntity).Name).ReplaceOneAsync(filter, obj);
        }

        public void Remove(Expression<Func<TEntity, bool>> filter)
        {
            database.GetCollection<TEntity>(typeof(TEntity).Name).DeleteMany(filter);
        }
    }
}
