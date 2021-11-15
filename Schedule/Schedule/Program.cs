using Schedule.CheckEvent;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity;

namespace Schedule
{
    public class Program
    {
        /// <summary>
        /// 反射取得所有實作ICheckPerDay的型別
        /// </summary>
        private static readonly IEnumerable<Type> ICheckPerDayTypes = TypeExtension.AllAssignableFromInterface(typeof(ICheckPerDay));

        static void Main(string[] args)
        {
            //注入容器
            UnityContainer unityContainer = new UnityContainer();

            //排程工具
            Schedule schedule = new Schedule(950);

            //所有的ICheckPerDay放進schedule
            RefreshMission(schedule, unityContainer);

            //自定義Action
            schedule.AssignMission(() => Console.WriteLine("自定義任務於10:00進行"),
                                        "10:00");

#if DEBUG
            //立即執行今日已過期任務
            schedule.ExpiredInvoke();
#endif
            //日更任務
            SetDailyRefresh(schedule, unityContainer, "00:00");

            while (true)
            {
                Console.ReadKey();
                Console.WriteLine($"\r{DateTime.Now}: ");
            }
        }

        /// <summary>
        /// 放入所有的ICheckPerDay任務
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="unityContainer"></param>
        private static void RefreshMission(Schedule schedule, UnityContainer unityContainer)
        {
            foreach (var type in ICheckPerDayTypes)
            {
                var item = (ICheckPerDay)unityContainer.Resolve(type);
                var trigger = type.GetCustomAttribute<TriggerAttribute>();
                schedule.AssignMission(item.Mission, trigger.Time);
            }
        }

        /// <summary>
        /// 每日獲取新任務
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="unityContainer"></param>
        /// <param name="resetTime"></param>
        private static void SetDailyRefresh(Schedule schedule, UnityContainer unityContainer, string resetTime)
        {
            schedule.Clear();
            RefreshMission(schedule, unityContainer);
            Console.WriteLine("完成每日刷新任務：");
            schedule.AssignMission(() => SetDailyRefresh(schedule, unityContainer, resetTime),
                                   resetTime);
        }
    }
}
