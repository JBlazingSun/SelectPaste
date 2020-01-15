using NUnit.Framework;
using SelectPaste.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectPaste.Service.Tests
{
    [TestFixture()]
    public class SetTimeTests
    {
        [Test()]
        public void GetUnixBeijingTimeTest()
        {
            var setTime = new SetTime();
            var unixBeijingTime = setTime.GetUnixBeijingTime();

        }


        [Test()]
        public void SetTimeFuncTest()
        {
            var setTime = new SetTime();
            setTime.SetTimeFunc(1564033137);
        }

        [Test()]
        public void ifTimeDiffTest()
        {
            var dt1 = DateTime.Now;
            var dt2 = dt1.Month;


            // DateTime dt2 = DateTime.Now.AddHours(24);
            //
            //
            // TimeSpan ts = dt2.Subtract(dt1);
            //
            //
            // var hours = (int)ts.TotalDays;

        }
    }
}