using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MongoRecord
    {
        private String collection;
        private BsonDocument content;

        public MongoRecord(String collection, BsonDocument content)
        {
            this.collection = collection;
            this.content = content;
        }

        public String getCollection()
        {
            return collection;
        }

        public BsonDocument getContent()
        {
            return content;
        }
    }
}
