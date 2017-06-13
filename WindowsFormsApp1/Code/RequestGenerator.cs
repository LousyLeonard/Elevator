using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class RequestGenerator
    {
        private int minFloor;
        private int maxFloor;

        private Random randomGenerator;

        public RequestGenerator(int minFloor, int maxFloor)
        {
            this.minFloor = minFloor;
            this.maxFloor = maxFloor;

            this.randomGenerator = new Random();
        }

        public Person getNewRequest()
        {
            int currentFloor = getRandomNumber();
            int desiredFloor;
            do
            {
                desiredFloor = getRandomNumber();
            } while (desiredFloor == currentFloor);

            return new Person(currentFloor, desiredFloor);
        }

        private int getRandomNumber()
        {
            return randomGenerator.Next(minFloor, maxFloor + 1);
        }
    }
}
