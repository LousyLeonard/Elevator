using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /**
     * Variant on both the stop everywhere and the queue algorithm.
     * It moves between floors in the order requested but stops at each floor 
     * on the way.
     * This variant favours fulfilling the wants of people already in the elevator.
     */
    class StopEverywhereEmptyQueueAlgorithm : IElevatorAlgorithm
    {
        private static String ALGORITHM_NAME = "Queue Algorithm";

        private Elevator elevator;

        public StopEverywhereEmptyQueueAlgorithm()
        {
            // Do Nothing.
        }

        public int getNextFloor()
        {
            // Let people off the elevator who are on first.
            if (this.elevator.getPeopleOnElevator().Any())
            {
                return this.elevator.getPeopleOnElevator().First().getDesiredFloor();
            }
            // Then try to fulfill any requests to get on.
            else if (this.elevator.getElevatorRequests().Any())
            {
                return this.elevator.getElevatorRequests().First().getDesiredFloor();
            }
            // If neither sit still.
            else
            {
                return -1;
            }
        }

        public string getName()
        {
            return ALGORITHM_NAME;
        }

        public void setElevator(Elevator elevator)
        {
            this.elevator = elevator;
        }
    }
}
