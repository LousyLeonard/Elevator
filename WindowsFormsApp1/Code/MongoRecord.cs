using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MongoRecord : IRecord
    {
        private String collection;
        private IRecord content;

        public MongoRecord(String collection, IRecord content)
        {
            this.collection = collection;
            this.content = content;
        }

        public String getCollection()
        {
            return collection;
        }

        public BsonDocument getRecord()
        {
            return content.getRecord();
        }
    }
}
