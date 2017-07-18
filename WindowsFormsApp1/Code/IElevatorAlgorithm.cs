﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    interface IElevatorAlgorithm
    {
        int getNextFloor();

        void setElevator(Elevator elevator);

        String getName();
    }
}
