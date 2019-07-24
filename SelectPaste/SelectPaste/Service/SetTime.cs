using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SelectPaste.Service
{
    public class SetTime
    {
        public void SetTimeFunc(double TimeUnix)
        {
            //取得当前系统时间
            DateTime t = ConvertIntDateTime(TimeUnix);
            //在当前时间上加上一周
            t = t.AddDays(7);
            
            //转换System.DateTime到SYSTEMTIME
            SYSTEMTIME st = new SYSTEMTIME();
            st.FromDateTime(t);
            
            //调用Win32 API设置系统时间
            Win32API.SetLocalTime(ref st);
        }
        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }
        public struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

            /// <summary>
            /// 从System.DateTime转换。
            /// </summary>
            /// <param name="time">System.DateTime类型的时间。</param>
            public void FromDateTime(DateTime time)
            {
                wYear = (ushort)time.Year;
                wMonth = (ushort)time.Month;
                wDayOfWeek = (ushort)time.DayOfWeek;
                wDay = (ushort)time.Day;
                wHour = (ushort)time.Hour;
                wMinute = (ushort)time.Minute;
                wSecond = (ushort)time.Second;
                wMilliseconds = (ushort)time.Millisecond;
            }
            /// <summary>
            /// 转换为System.DateTime类型。
            /// </summary>
            /// <returns></returns>
            public DateTime ToDateTime()
            {
                return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
            }
            /// <summary>
            /// 静态方法。转换为System.DateTime类型。
            /// </summary>
            /// <param name="time">SYSTEMTIME类型的时间。</param>
            /// <returns></returns>
            public static DateTime ToDateTime(SYSTEMTIME time)
            {
                return time.ToDateTime();
            }
        }
        public class Win32API
        {
            [DllImport("Kernel32.dll")]
            public static extern bool SetLocalTime(ref SYSTEMTIME Time);
            [DllImport("Kernel32.dll")]
            public static extern void GetLocalTime(ref SYSTEMTIME Time);
        }
    }
}
