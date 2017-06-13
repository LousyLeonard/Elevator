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
    class ExpiryRecorder : MongoClient, ISubscriber
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public ExpiryRecorder(String serverLocation)
        { 
            _client = new MongoClient(serverLocation);
            _database = _client.GetDatabase("test");

            _client.Cluster.DescriptionChanged += Cluster_DescriptionChanged;

            Person.subscribe(this);
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

        public void insertPersonAsync(Person person)
        {
            var document = new BsonDocument
            {
                { "originFloor", person.getCurrentFloor() },
                { "destinationFloor", person.getDesiredFloor() },
                { "startTime", person.getCreationTime() },
                { "endTime", DateTime.UtcNow.Ticks }
            };

            var collection = _database.GetCollection<BsonDocument>("persons");
            collection.InsertOneAsync(document);
        }

        public void update(UpdateOptions updateType, object obj)
        {
            if (updateType == UpdateOptions.PersonExpired)
                insertPersonAsync((Person)(obj));
        }
    }
}
