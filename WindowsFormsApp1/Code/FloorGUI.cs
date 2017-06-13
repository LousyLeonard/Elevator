using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class FloorGUI : Panel
    {
        private Label leftLabel;
        private Label middleLabel;
        private Label rightLabel;
        private Panel elevatorPanel;

        public FloorGUI()
        {
            // Set up the panel
            this.elevatorPanel = new Panel();
            this.elevatorPanel.Size = new System.Drawing.Size(ElevatorGUI.FLOOR_LENGTH, ElevatorGUI.FLOOR_HEIGHT);
            this.setNoElevatorHere();
            this.elevatorPanel.Location = new System.Drawing.Point(30, 0);
            
            this.leftLabel = new Label();
            this.leftLabel.Location = new System.Drawing.Point(0, 0);
            this.leftLabel.Text = "emptyl";
            this.leftLabel.Size = new System.Drawing.Size(40, 20);

            this.middleLabel = new Label();
            this.middleLabel.Location = new System.Drawing.Point(25, 0);
            this.middleLabel.Text = "emptym";
            this.middleLabel.Size = new System.Drawing.Size(40, 20);

            this.rightLabel = new Label();
            this.rightLabel.Location = new System.Drawing.Point(125, 0);
            this.rightLabel.Text = "emptyr";
            this.rightLabel.Size = new System.Drawing.Size(40, 20);

            this.Controls.Add(leftLabel);
            elevatorPanel.Controls.Add(middleLabel);
            this.Controls.Add(elevatorPanel);
            this.Controls.Add(rightLabel);
        }

        public Label getLeftLabel()
        {
            return leftLabel;
        }

        public Label getMiddelLabel()
        {
            return middleLabel;
        }

        public Label getRightLabel()
        {
            return rightLabel;
        }

        public void setNoElevatorHere()
        {
            this.elevatorPanel.BackColor = Color.FromArgb(0, 232, 232);
        }

        public void setElevatorHere()
        {
            this.elevatorPanel.BackColor = Color.FromArgb(232, 0, 232);
        }
    }
}
