using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface IElevatorAlgorithm
    {
        int getNextFloor();

        void addEntry(Person entry);

        void addEntries(List<Person> entries);

        List<Person> arrivedAtFloor(int atFloor);

        void setFloors(List<Floor> floors);

        String getName();
    }
}
