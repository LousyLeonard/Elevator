using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ElevatorPublisher : IPublisher
    {
        private List<ISubscriber> subscribers;

        protected String collectionName;

        public ElevatorPublisher()
        {
            subscribers = new List<ISubscriber>();

            this.subscribe(DatabaseWriter.getInstance());
        }

        public void subscribe(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        protected void sendNotification(UpdateOptions type, IRecord record)
        {
            MongoRecord mongoRecord = new MongoRecord(collectionName, record);

            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.receiveNotification(type, mongoRecord);
            }
        }

        protected void sendNotification(UpdateOptions type, IEnumerable<IRecord> records)
        {
            foreach (IRecord record in records)
            {
                this.sendNotification(type, record);
            }
        }

        protected void sendNotification(UpdateOptions type)
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.receiveNotification(type, null);
            }
        }

    }
}
