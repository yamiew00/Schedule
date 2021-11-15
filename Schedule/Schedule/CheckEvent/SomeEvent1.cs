using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.CheckEvent
{
    [Trigger("8:35")]
    public class SomeEvent1 : ICheckPerDay
    {
        public Action Mission => () =>
        {
            Console.WriteLine($"{nameof(SomeEvent1)}執行了。");
        };
    }
}
