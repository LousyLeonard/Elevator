using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Core.Clusters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class DatabaseWriter : MongoClient, ISubscriber
    {
        protected static DatabaseWriter instance;

        protected static IMongoClient _client;
        protected static IMongoDatabase _database;
        protected static String _server;

        protected static Dictionary<String, List<BsonDocument>> _cache;

        private DatabaseWriter()
        {
            _server = "mongodb://192.168.1.13:27017";

            _client = new MongoClient(_server);
            _database = _client.GetDatabase("Elevators");

            _cache = new Dictionary<String, List<BsonDocument>>();

            _client.Cluster.DescriptionChanged += Cluster_DescriptionChanged;
        }

        public static DatabaseWriter getInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseWriter();
            }
            return instance;
        }

        public void Cluster_DescriptionChanged(object sender, ClusterDescriptionChangedEventArgs e)
        {
            switch (e.NewClusterDescription.State)
            {
                case ClusterState.Disconnected:
                    Console.WriteLine("Disconnected from mongo db");
                    break;
                case ClusterState.Connected:
                    Console.WriteLine("Successfully connected to mongo db");
                    break;
            }
        }

        public void receiveNotification(UpdateOptions updateType, object obj)
        {
            switch (updateType)
            {
                case UpdateOptions.GetOff:
                case UpdateOptions.GetOn:
                case UpdateOptions.RequestStart:
                    addRecord((MongoRecord)(obj));
                    break;
            }
        }

        private void addRecord(MongoRecord record)
        {
            if (!_cache.ContainsKey(record.getCollection()))
            {
                _cache.Add(record.getCollection(), new List<BsonDocument>());
            }

            List<BsonDocument> entries = _cache[record.getCollection()];
            entries.Add(record.getRecord());
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(10000))
            {
                foreach (KeyValuePair<String, List<BsonDocument>> collection in _cache)
                {
                    var mongoCollection = _database.GetCollection<BsonDocument>(collection.Key);
                    mongoCollection.InsertManyAsync(collection.Value);
                }

                _cache.Clear();
            }
        }
    }
}
