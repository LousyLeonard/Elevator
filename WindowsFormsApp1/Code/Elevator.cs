using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Elevator : IPublisher
    {
        private int currentFloor;
        private int capacity;
        private String name;

        private List<ISubscriber> subscribers;

        private IElevatorAlgorithm algorithm;

        private Dictionary<int, Floor> floors;

        public Elevator(List<Floor> floors, IElevatorAlgorithm algorithm, String name)
        {
            this.subscribers = new List<ISubscriber>();
            this.name = name;

            this.floors = new Dictionary<int, Floor>();
            foreach (Floor floor in floors)
            {
                this.floors.Add(floor.getFloorNo(), floor);
            }

            this.currentFloor = 1;
            this.capacity = 5;

            this.algorithm = algorithm;
            this.algorithm.setFloors(floors);
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(1500))
            {
                algorithm.addEntries(floors[currentFloor].getPeopleWaiting());
                algorithm.atFloor(currentFloor);

                int getNextFloor = algorithm.getNextFloor();
                if (getNextFloor == -1)
                {
                    getNextFloor = currentFloor;
                }

                if (getNextFloor < currentFloor)
                {
                    moveDown();
                    Console.WriteLine("down command issued");
                }
                else if (getNextFloor > currentFloor)
                {
                    moveUp();
                    Console.WriteLine("up command issued");
                }
            }
        }

        public void requestElevator(Person person)
        {
            floors[person.getCurrentFloor()].addToFloor(person);
        }

        private void moveUp()
        {
            notify(UpdateOptions.MoveUp);
            currentFloor++;
        }

        private void moveDown()
        {
            notify(UpdateOptions.MoveDown);
            currentFloor--;
        }

        public int getMinFloor()
        {
            if (floors.Any())
                return 1;
            else
                return 0;
        }

        public int getMaxFloor()
        {
            return floors.Count;
        }

        public List<Floor> getFloors()
        {
            return floors.Values.ToList();
        }

        public void subscribe(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        private void notify(UpdateOptions option)
        {
            foreach (ISubscriber subscriber in subscribers)
            {
                subscriber.update(option, null);
            }
        }

        public String getName()
        {
            return this.name;
        }
    }
}
