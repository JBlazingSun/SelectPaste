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
    }
}