using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.CheckEvent
{    
    /// <summary>
    /// 有每日任務
    /// </summary>
    public interface ICheckPerDay
    {
        /// <summary>
        /// 每日任務
        /// </summary>
        public Action Mission { get; }
    }
}
