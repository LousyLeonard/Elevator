using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class PersonLabel : Label
    {
        private Person person;
        private LabelAnimator animator;

        public PersonLabel(Person person)
        {
            this.person = person;

            this.Size = new System.Drawing.Size(20, 10);
            this.Text = person.getDesiredFloor().ToString();
            this.Location = new System.Drawing.Point(0, (person.getCurrentFloor() * ElevatorGUI.FLOOR_SEPERATION) - ElevatorGUI.FLOOR_HEIGHT / 2 - 5);

            animator = new LabelAnimator();

            // Move right 40 pixels in 2 seconds.
            animator.StoreAnimationInfo(this, 40, 0, 1000);
        }

        public void getOn()
        {
            /*
            long start = DateTime.UtcNow.Ticks;
            int offset = 0;
            int iterationWait = 1000 / 75;

            for (long i = start; i < start + TimeSpan.TicksPerSecond; i = DateTime.UtcNow.Ticks)
            {
                this.Location = new System.Drawing.Point(offset++, person.getCurrentFloor() * UserControl1.FLOOR_SEPERATION);

                System.Threading.Thread.Sleep(iterationWait);
            }*/
            animator.btnAnimate_Click();
        }

        public void getOff()
        {
            long start = DateTime.UtcNow.Ticks;
            int offset = 0;
            int iterationWait = 1000 / 75;

            for (long i = start; i < start + TimeSpan.TicksPerSecond; i = DateTime.UtcNow.Ticks)
            {
                this.Location = new System.Drawing.Point(offset++, person.getCurrentFloor() * ElevatorGUI.FLOOR_SEPERATION);

                System.Threading.Thread.Sleep(iterationWait);
            }
        }
    }
}
