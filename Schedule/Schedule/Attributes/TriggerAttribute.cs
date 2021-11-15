using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    /// <summary>
    /// 觸發時間。24小時制(00:00~23:59)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TriggerAttribute : Attribute
    {
        /// <summary>
        /// 時間
        /// </summary>
        public TimeSpan Time { get; }

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="timeString">時間字串</param>
        public TriggerAttribute(string timeString)
        {
            Time = timeString.ToTimeSpan();
        }
    }
}
