using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization.Configuration;
using Newtonsoft.Json;
using SelectPaste.Model;

namespace SelectPaste.Service
{
    public class SetTime
    {
        public void SetTimeFunc(double TimeUnix)
        {
            //取得当前系统时间
            DateTime t = ConvertIntDateTime(TimeUnix);

            //转换System.DateTime到SYSTEMTIME
            SYSTEMTIME st = new SYSTEMTIME();
            st.FromDateTime(t);
            
            //调用Win32 API设置系统时间
            Win32API.SetLocalTime(ref st);
        }

        public int GetUnixBeijingTime()
        {
            var ret = 1564033137;

            var Url = "http://api.m.taobao.com/rest/api3.do?api=mtop.common.getTimestamp";
            WebRequest wReq = WebRequest.Create(Url);

            WebResponse wResp = wReq.GetResponse();

            Stream respStream = wResp.GetResponseStream();
            StreamReader reader = new StreamReader(respStream, Encoding.GetEncoding("UTF-8"));
            //result就是返回值
            var result = reader.ReadToEnd();

            var rootobject = JsonConvert.DeserializeObject<Rootobject>(result);
            
            try
            {
                ret = (rootobject.data.t.ToDouble() / 1000).ToInt32();
            }
            catch (Exception e)
            {
            }

            return ret;
        }

        public bool ifTimeDiff()
        {
            //是否为第一次启动,< 0 是
            var isFirstStart = DateTime.Compare(SelectPaste_From.anchorDateTime, new DateTime(1991, 6, 18));
            if (isFirstStart == 0)
            {
                SelectPaste_From.anchorDateTime = DateTime.Now;
                SelectPaste_From.ctDateTime = SelectPaste_From.anchorDateTime;
                return true;
            }
            //计算时间差，确定是否过了一天
            TimeSpan ts = SelectPaste_From.ctDateTime.Subtract(SelectPaste_From.anchorDateTime);
            var hours = (int)ts.TotalDays;
            if (hours >= 1)
            {
                SelectPaste_From.anchorDateTime = DateTime.Now;
                SelectPaste_From.ctDateTime = SelectPaste_From.anchorDateTime;
                return true;
            }

            return false;
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
