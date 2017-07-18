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
        private List<Person> elevatorRequests;

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
            return peopleOnElevator;
        }

        public List<Person> getElevatorRequests()
        {
            return elevatorRequests;
        }

        private void peopleOn(List<Person> people)
        {
            foreach (Person person in people)
            {
                // Person gets on the elevator.
                peopleOnElevator.Add(person);
                // The request is fulfilled.
                elevatorRequests.Remove(person);
            }

            this.sendNotification(UpdateOptions.GetOn, people);
        }

        private void peopleOff()
        {
            List<Person> peopleLeft = peopleOnElevator;
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
        }

        private void figureOutwhereToGoNext()
        {
            int nextFloor = algorithm.getNextFloor();

            if (nextFloor == STAY_AT_THIS_FLOOR)
            {
                // Do nothing
            }
            else if (nextFloor < currentFloor)
            {
                goDownAFloor();
            }
            else if (nextFloor > currentFloor)
            {
                goUpAFloor();
            }
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(1500))
            {
                this.sendNotification(UpdateOptions.AtFloor, this);

                this.peopleOff();
                this.peopleOn(floors[currentFloor].getPeopleWaiting());

                this.figureOutwhereToGoNext();
            }
        }

        public void requestElevator(Person person)
        {
            floors[person.getCurrentFloor()].addToFloor(person);
            this.sendNotification(UpdateOptions.RequestStart, person);
            this.elevatorRequests.Add(person);
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
