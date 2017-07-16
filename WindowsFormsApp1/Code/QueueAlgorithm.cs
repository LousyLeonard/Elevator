using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class QueueAlgorithm : IElevatorAlgorithm
    {
        private static String ALGORITHM_NAME = "Queue Algorithm";

        private List<Person> people;
        private List<Floor> floors;

        public QueueAlgorithm() : base()
        {
            people = new List<Person>();
            floors = new List<Floor>();
        }

        public int getNextFloor()
        {
            if (people.Any())
            {
                return people.First().getDesiredFloor();
            }
            else
            {
                int floorToVisit = -1;
                foreach (Floor floor in floors)
                {
                    foreach (Person person in floor.checkPeopleWaiting())
                    {
                        floorToVisit = person.getCurrentFloor();
                        break;
                    }

                    if (floorToVisit != -1)
                    {
                        break;
                    }
                }

                return floorToVisit;
            }
        }

        public void addEntry(Person entry)
        {
            addEntries(new List<Person> { entry });
        }

        public void addEntries(List<Person> entries)
        {
            foreach (Person person in entries)
            {
                people.Add(person);
            }
        }

        public List<Person> arrivedAtFloor(int atFloor)
        {
            List<Person> peopleLeft = people;
            List<Person> peopleOff = new List<Person>();

            foreach (Person person in people)
            {
                if (person.getDesiredFloor() == atFloor)
                {
                    peopleLeft.Remove(person);
                    peopleOff.Add(person);
                }
            }

            people = peopleLeft;
            return peopleLeft;
        }

        public void setFloors(List<Floor> floors)
        {
            this.floors = floors;
        }

        public string getName()
        {
            return ALGORITHM_NAME;
        }
    }
}
