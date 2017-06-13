using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Person
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
