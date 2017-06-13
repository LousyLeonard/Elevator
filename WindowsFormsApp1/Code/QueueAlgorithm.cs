using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class QueueAlgorithm : IElevatorAlgorithm
    {
        private List<int> floorsToVisit;
        private List<Floor> floors;

        public QueueAlgorithm()
        {
            floorsToVisit = new List<int>();
            floors = new List<Floor>();
        }

        public int getNextFloor()
        {
            if (floorsToVisit.Any())
            {
                return floorsToVisit.First();
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
                floorsToVisit.Add(person.getDesiredFloor());
            }
        }

        public void atFloor(int atFloor)
        {
            List<int> newFloorsToVisit = new List<int>();

            foreach (int floor in floorsToVisit)
            {
                if (floor != atFloor)
                {
                    newFloorsToVisit.Add(floor);
                }
                else
                {
                    Console.WriteLine("One got off at floor : " + atFloor);
                }
            }

            floorsToVisit = newFloorsToVisit;
        }

        public void setFloors(List<Floor> floors)
        {
            this.floors = floors;
        }
    }
}
