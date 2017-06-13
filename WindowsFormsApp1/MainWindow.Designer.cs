using System.Drawing;

namespace WindowsFormsApp1
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.elevatorGUI = new WindowsFormsApp1.ElevatorGUI(elevator);
            this.SuspendLayout();
            // 
            // elevatorGUI
            // 
            this.elevatorGUI.Location = new System.Drawing.Point(30, 30);
            this.elevatorGUI.Name = elevator.getName();
            this.elevatorGUI.Size = new System.Drawing.Size(
                    ElevatorGUI.FLOOR_LENGTH * 2, ElevatorGUI.FLOOR_SEPERATION * elevator.getFloors().Count);
            this.elevatorGUI.TabIndex = 5;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(
                ElevatorGUI.FLOOR_LENGTH * 2 + 60, ElevatorGUI.FLOOR_SEPERATION * elevator.getFloors().Count + 30);
            this.Controls.Add(this.elevatorGUI);
            this.Name = "ElevatorSim";
            this.Text = "ElevatorSim";
            this.ResumeLayout(false);
        }

        #endregion

        private ElevatorGUI elevatorGUI;
    }
}

