﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public interface ISubscriber
    {
        void receiveNotification(UpdateOptions updateType, Object obj);
    }
}
