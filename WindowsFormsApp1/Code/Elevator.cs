using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace WindowsFormsApp1
{
    class Elevator : ElevatorPublisher, IRequestable, IRecord
    {
        private static readonly int STAY_AT_THIS_FLOOR = -1;

        private int currentFloor;
        private int capacity;
        private String name;

        private List<Person> peopleOnElevator;
        private volatile List<Person> elevatorRequests;

        private IElevatorAlgorithm algorithm;

        private Dictionary<int, Floor> floors;

        public Elevator(List<Floor> floors, IElevatorAlgorithm algorithm, String name) : base()
        {
            this.name = name;

            this.floors = new Dictionary<int, Floor>();
            foreach (Floor floor in floors)
            {
                this.floors.Add(floor.getFloorNo(), floor);
            }

            this.currentFloor = 1;
            this.capacity = 5;

            this.peopleOnElevator = new List<Person>();
            this.elevatorRequests = new List<Person>();

            this.algorithm = algorithm;
            this.algorithm.setElevator(this);
            this.collectionName = algorithm.getName();
        }

        public List<Person> getPeopleOnElevator()
        {
            return this.peopleOnElevator;
        }

        public List<Person> getElevatorRequests()
        {
            return this.elevatorRequests;
        }

        private void peopleOn(List<Person> peopleOn)
        {
            foreach (Person person in peopleOn)
            {
                // Person gets on the elevator.
                this.peopleOnElevator.Add(person);
                // The request is fulfilled.
                this.elevatorRequests.Remove(person);
            }

            this.sendNotification(UpdateOptions.GetOn, peopleOn);
            Console.WriteLine("{0} People got on.", peopleOn.Count);
        }

        private void peopleOff()
        {
            List<Person> peopleLeft = new List<Person>(peopleOnElevator);
            List<Person> peopleOff = new List<Person>();

            foreach (Person person in peopleOnElevator)
            {
                if (person.getDesiredFloor() == currentFloor)
                {
                    peopleLeft.Remove(person);
                    peopleOff.Add(person);
                }
            }

            peopleOnElevator = peopleLeft;

            this.sendNotification(UpdateOptions.GetOff, peopleOff);
            Console.WriteLine("{0} People got off.", peopleLeft.Count);
        }

        private void figureOutwhereToGoNextAndGoThere()
        {
            int nextFloor = algorithm.getNextFloor();

            if (nextFloor == STAY_AT_THIS_FLOOR)
            {
                // Do Nothing
            }
            else if (nextFloor == currentFloor)
            {
                openDoor();
            }
            else if (nextFloor < currentFloor)
            {
                goDownAFloor();
            }
            else if (nextFloor > currentFloor)
            {
                goUpAFloor();
            }

            this.sendNotification(UpdateOptions.AtFloor, this);
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(1500))
            {
                this.figureOutwhereToGoNextAndGoThere();
            }
        }

        public void requestElevator(Person person)
        {
            this.sendNotification(UpdateOptions.RequestStart, person);

            this.floors[person.getCurrentFloor()].addToFloor(person);
            this.elevatorRequests.Add(person);

            Console.WriteLine("Elevator requested from {0}", person.getCurrentFloor());
        }

        private void goUpAFloor()
        {
            this.sendNotification(UpdateOptions.MoveUp);
            currentFloor++;
            Console.WriteLine("Up command issued. Moved from {0} to {1}", currentFloor - 1, currentFloor);
        }

        private void goDownAFloor()
        {
            this.sendNotification(UpdateOptions.MoveDown);
            currentFloor--;
            Console.WriteLine("Down command issued. Moved from {0} to {1}", currentFloor + 1, currentFloor);
        }

        private void openDoor()
        {
            this.sendNotification(UpdateOptions.OpenDoor);
            Console.WriteLine("Opening doors");

            peopleOff();
            peopleOn(this.floors[currentFloor].getPeopleWaiting());
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

        public BsonDocument getRecord()
        {
            var people = new BsonArray();

            foreach (Person person in peopleOnElevator)
            {
                people.Add(person.getRecord());
            }

            var document = new BsonDocument {
                { "ElevatorReport", new BsonDocument {
                        { "currentFloor", this.currentFloor },
                        { "currentTime", DateTime.UtcNow.Ticks },
                        { "peopleOnElevator", people }
                    }
                }
            };

            return document;
        }
    }
}
