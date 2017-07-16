using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Elevator : ElevatorPublisher, IRequestable
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
            this.collectionName = algorithm.getName();
        }

        private void peopleOn(List<Person> people)
        {
            this.algorithm.addEntries(people);
            this.sendNotification(UpdateOptions.GetOn, people);
        }

        private void peopleOff(List<Person> people)
        {
            this.sendNotification(UpdateOptions.GetOn, people);
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(1500))
            {
                this.peopleOn(floors[currentFloor].getPeopleWaiting());
                this.peopleOff(algorithm.arrivedAtFloor(currentFloor));

                int getNextFloor = algorithm.getNextFloor();
                if (getNextFloor == -1)
                {
                    getNextFloor = currentFloor;
                }

                if (getNextFloor < currentFloor)
                {
                    goDownAFloor();
                }
                else if (getNextFloor > currentFloor)
                {
                    goUpAFloor();
                }
            }
        }

        public void requestElevator(Person person)
        {
            floors[person.getCurrentFloor()].addToFloor(person);
            this.sendNotification(UpdateOptions.RequestStart, person);
        }

        private void goUpAFloor()
        {
            this.sendNotification(UpdateOptions.MoveUp);
            currentFloor++;
            Console.WriteLine("up command issued");
        }

        private void goDownAFloor()
        {
            this.sendNotification(UpdateOptions.MoveDown);
            currentFloor--;
            Console.WriteLine("down command issued");
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

        public String getName()
        {
            return this.name;
        }
    }
}
