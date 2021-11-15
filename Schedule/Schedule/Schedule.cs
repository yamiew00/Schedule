using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Schedule
{
    public class Schedule
    {
        /// <summary>
        /// 內建的Timer
        /// </summary>
        private readonly Timer Timer;

        public int Count
        {
            get
            {
                return List.Count;
            }
        }

        public Schedule(double interval)
        {
            //Timer設定
            Timer = new Timer();
            Timer.Interval = interval;
            Timer.Elapsed += new ElapsedEventHandler(PerformActions);

            Timer.Start();
        }

        /// <summary>
        /// 今日任務清單
        /// </summary>
        List<DailyMission> List = new List<DailyMission>();

        public void AssignMission(Action action, string timeString = "", string missionName = "")
        {
            //todo: ToTimeSpan別放TimeExtension，應在Clock?
            List.Add(new DailyMission(action,
                                    timeString.ToTimeSpan(),
                                    missionName));
        }

        public void AssignMission(Action action, TimeSpan timeSpan, string missionName = "")
        {
            List.Add(new DailyMission(action,
                                    timeSpan,
                                    missionName));
        }

        private void PerformActions(object sender, ElapsedEventArgs e)
        {
            TimeSpan now = Clock.HHMMSS;

            List<DailyMission> tempList = new List<DailyMission>();

            //找出所有應執行而未執行的任務
            //有些action會執行很久，為了在下個interval來臨前能正確判斷IsComplete，需要優先更改所有IsComplete。
            foreach (var mission in List)
            {
                if (mission.TimeSpan == now && !mission.IsComplete)
                {
                    mission.IsComplete = true;
                    tempList.Add(mission);
                }
            }

            //執行所有應執行mission
            foreach (var mission in tempList)
            {
                mission.Invoke();
            }
        }

        /// <summary>
        /// 執行目前應完成的任務。如果現在是4am，則會執行所有0~4am的任務。
        /// </summary>
        public void ExpiredInvoke()
        {
            TimeSpan now = Clock.HHMMSS;

            Console.WriteLine($"立即執行 {List.Where(mission => now >= mission.TimeSpan).Count()} 個任務：");

            List.ForEach(mission =>
            {
                if (now >= mission.TimeSpan && mission.TimeSpan > TimeSpan.Zero)
                {
                    mission.Invoke();
                }
            });
        }

        public void Clear()
        {
            List.Clear();
        }
    }
}
