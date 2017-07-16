using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Person : IRecord
    {
        private static List<ISubscriber> subscribers = new List<ISubscriber>();

        private int currentFloor;
        private int desiredFloor;

        private long creationTime;

        public Person(int currentFloor, int desiredFloor)
        {
            this.currentFloor = currentFloor;
            this.desiredFloor = desiredFloor;

            creationTime = DateTime.UtcNow.Ticks;
        }

        ~Person()
        {
            // Consider using a person pool
            notify(UpdateOptions.PersonExpired, this);
        }

        public BsonDocument getRecord()
        {
            var document = new BsonDocument
            {
                { "originFloor", this.getCurrentFloor() },
                { "destinationFloor", this.getDesiredFloor() },
                { "startTime", this.getCreationTime() },
                { "endTime", DateTime.UtcNow.Ticks }
            };

            return document;
        }

        public int getCurrentFloor()
        {
            return currentFloor;
        }

        public int getDesiredFloor()
        {
            return desiredFloor;
        }

        public long getCreationTime()
        {
            return creationTime;
        }

        public static void subscribe(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void notify(UpdateOptions option, Object obj)
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.update(option, obj);
            }
        }
    }
}
