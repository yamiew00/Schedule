using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public class DailyMission
    {
        /// <summary>
        /// 任務名稱
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 任務內容
        /// </summary>
        public Action Mission { get; }


        /// <summary>
        /// 執行時間
        /// </summary>
        public TimeSpan TimeSpan { get; }

        /// <summary>
        /// 已完成
        /// </summary>
        public bool IsComplete { get; set; }

        public DailyMission(Action mission, TimeSpan timeSpan, string name = "", bool isComplete = false)
        {
            Name = name;
            Mission = mission;
            TimeSpan = timeSpan;
            IsComplete = isComplete;
        }

        public void Invoke()
        {
            Mission.Invoke();
        }
    }
}
