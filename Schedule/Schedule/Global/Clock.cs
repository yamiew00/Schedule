using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public static class Clock
    {
        /// <summary>
        /// 回傳現在的時分秒。
        /// </summary>
        public static TimeSpan HHMMSS
        {
            get
            {
                DateTime dateTime = DateTime.Now;
                return TimeSpan.FromHours(dateTime.Hour)
                               .Add(TimeSpan.FromMinutes(dateTime.Minute))
                               .Add(TimeSpan.FromSeconds(dateTime.Second));
            }
        }

        public static DayOfWeek DayOfWeek
        {
            get
            {
                DateTime dateTime = DateTime.Now;
                return dateTime.DayOfWeek;
            }
        }

        /// <summary>
        /// 時間字串轉成TimeSpan。只支援"hh"、"hh:mm"、"hh:mm:ss"
        /// </summary>
        /// <param name="timeString">時間字串</param>
        /// <returns>timeSpan</returns>
        public static TimeSpan ToTimeSpan(this string timeString)
        {
            var times = timeString.Split(":");

            if (times.Length == 1)
            {
                return TimeSpan.FromHours(int.Parse(times[0]));
            }
            else if (times.Length == 2)
            {
                return TimeSpan.FromHours(int.Parse(times[0]))
                               .Add(TimeSpan.FromMinutes(int.Parse(times[1])));
            }
            if (times.Length == 3)
            {
                return TimeSpan.FromHours(int.Parse(times[0]))
                               .Add(TimeSpan.FromMinutes(int.Parse(times[1])))
                               .Add(TimeSpan.FromSeconds(int.Parse(times[2])));
            }
            throw new Exception("ToTimeSpan throws error");
        }
    }
}
