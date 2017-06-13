using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Floor
    {
        private List<Person> peopleWaiting;

        private int floorNo;

        public Floor(int floorNo)
        {
            this.peopleWaiting = new List<Person>();
            this.floorNo = floorNo;
        }

        public void addToFloor(Person person)
        {
            peopleWaiting.Add(person);
        }

        public List<Person> checkPeopleWaiting()
        {
            return peopleWaiting;
        }

        public List<Person> getPeopleWaiting()
        {
            List<Person> result = this.peopleWaiting;
            this.peopleWaiting = new List<Person>();
            return result;
        }

        public int getFloorNo()
        {
            return floorNo;
        }
    }
}
