﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class MainWindow : Form
    {
        private Elevator elevator;

        public MainWindow(Elevator elevator)
        {
            this.elevator = elevator;

            InitializeComponent();
        }
    }
}
