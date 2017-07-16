using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class RequestGenerator
    {
        private int lowerConstraint;
        private int upperConstraint;

        private Random randomGenerator;

        private IRequestable requester;

        public RequestGenerator(IRequestable requester, int lowerConstraint, int upperConstraint)
        {
            this.requester = requester;
            this.lowerConstraint = lowerConstraint;
            this.upperConstraint = upperConstraint;

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
            return randomGenerator.Next(lowerConstraint, upperConstraint + 1);
        }

        public void Run()
        {
            for (; ; System.Threading.Thread.Sleep(1500))
            {
                requester.requestElevator(this.getNewRequest());
            }
        }
    }
}
