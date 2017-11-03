using AspNet.Identity.MongoDB;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreServer.Domain.Entities.Infrastructure
{
    public class DatabaseFactory
    {
        private readonly static string connectionString = ConfigurationManager.ConnectionStrings["MongoDatabaseConnection"].ConnectionString;
        private readonly static string databaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];

        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static DatabaseFactory()
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);

            var conventions = new ConventionPack();
            conventions.Add(new IgnoreExtraElementsConvention(true));
            ConventionRegistry.Register("IgnoreExtraElements", conventions, _ => true);
        }

        public static Tuple<IMongoCollection<ApplicationUser>, IMongoCollection<IdentityRole>> GetIdentityCollections()
        {
            var users = _database.GetCollection<ApplicationUser>("applicationusers");
            var roles = _database.GetCollection<IdentityRole>("identityroles");

            return new Tuple<IMongoCollection<ApplicationUser>, IMongoCollection<IdentityRole>>(users, roles);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
        }
    }
}
