﻿using System;
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

        void atFloor(int atFloor);

        void setFloors(List<Floor> floors);
    }
}