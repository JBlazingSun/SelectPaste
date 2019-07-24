using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelectPaste.Service
{
    public static class TimeTask
    {
        public static void TimeTaskThread()
        {
            while (true)
            {

                Thread.Sleep(1000*60);
            }
        }
    }
}
