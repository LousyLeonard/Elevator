using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ElevatorGUI : UserControl, ISubscriber
    {
        public static int FLOOR_SEPERATION = 40;
        public static int FLOOR_HEIGHT = 35;
        public static int FLOOR_LENGTH = 95;

        private Dictionary<Person, PersonLabel> personLabels;

        private Node<FloorGUI> currentPanel;

        public ElevatorGUI(Elevator elevator)
        {
            InitializeComponent();

            //this.SuspendLayout();

            this.personLabels = new Dictionary<Person, PersonLabel>();

            Node<FloorGUI> tempPrev = null;

            foreach (Floor floor in elevator.getFloors())
            {
                // Create the panel
                FloorGUI temp = new FloorGUI();

                // Set up the panel
                temp.Location = new System.Drawing.Point(30, floor.getFloorNo() * FLOOR_SEPERATION);

                // Wrap it in a node
                Node<FloorGUI> node = new Node<FloorGUI>(temp);
                if (floor.getFloorNo() != 1)
                    node.setPrev(tempPrev);
                else
                    currentPanel = node;
                tempPrev = node;

                // Add the panel to the gui
                this.Controls.Add(temp);
            }

            //this.ResumeLayout(true);

            elevator.subscribe(this);

        }

        public void getOnAtFloor(Person person)
        {
            PersonLabel temp = new PersonLabel(person);

            this.Controls.Add(temp);
            this.personLabels.Add(person, temp);

            temp.getOn();
        }

        public void getOffAtFloor(Person person)
        {
            PersonLabel temp = this.personLabels[person];

            temp.getOff();

            this.personLabels.Remove(person);
            this.Controls.Remove(temp);

            // Explicitly set person to null to encourage the garbage collector
            person = null;
        }

        private void moveUp()
        {
            currentPanel.getElement().setElevatorHere();

            currentPanel = currentPanel.getNext();

            currentPanel.getElement().setNoElevatorHere();
        }

        private void moveDown()
        {
            currentPanel.getElement().setNoElevatorHere();

            currentPanel = currentPanel.getPrev();

            currentPanel.getElement().setElevatorHere();
        }

        public void update(UpdateOptions updateType, object obj)
        {
            if (updateType == UpdateOptions.MoveUp)
            {
                moveUp();
            }
            else if (updateType == UpdateOptions.MoveDown)
            {
                moveDown();
            }
        }
    }
}
