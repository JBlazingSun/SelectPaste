﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SelectPaste.Service
{
    public static class TimeTask
    {
        public static void TimeTaskThread()
        {
            while (true)
            {
                MessageBox.Show("aaa".ToString());
                var setTime = new SetTime();
                var unixBeijingTime = setTime.GetUnixBeijingTime();
                setTime.SetTimeFunc(unixBeijingTime);
                MessageBox.Show(unixBeijingTime.ToString());
                Thread.Sleep(1000*30);
            }
        }
    }
}
