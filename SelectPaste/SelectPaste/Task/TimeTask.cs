using System;
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
                var setTime = new SetTime();
                var unixBeijingTime = setTime.GetUnixBeijingTime();
                setTime.SetTimeFunc(unixBeijingTime);
                Thread.Sleep(1000*30);
            }
        }
    }
}
