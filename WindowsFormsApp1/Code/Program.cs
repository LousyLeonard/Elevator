using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Floor> floors = new List<Floor>();
            floors.Add(new Floor(1));
//            floors.Add(new Floor(2));
//            floors.Add(new Floor(3));
//            floors.Add(new Floor(4));
//            floors.Add(new Floor(5));

            Elevator elevator = new Elevator(floors, new QueueAlgorithm(), "shaft1");

            Thread applicationThread = new Thread(Program.runApplication);
            applicationThread.Start(elevator);

            //Loading time
            System.Threading.Thread.Sleep(1500);

//            Thread requestThread = new Thread(Program.startRequestGenerator);
//            requestThread.Start(elevator);

//            Thread elevatorThread = new Thread(elevator.Run);
//            elevatorThread.Start();     
        }

        private static void runApplication(Object obj)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow((Elevator)obj));
        }

        private static void startRequestGenerator(Object obj)
        {
            Elevator elevator = (Elevator)obj;

            RequestGenerator reqGen = new RequestGenerator(elevator.getMinFloor(), elevator.getMaxFloor());

            for (; ; System.Threading.Thread.Sleep(1500))
            {
                elevator.requestElevator(reqGen.getNewRequest());
            }
        }
    }
}
