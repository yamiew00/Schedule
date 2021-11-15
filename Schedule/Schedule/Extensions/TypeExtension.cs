using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    /// <summary>
    /// Type相關擴充方法
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 找出所有實作@interface的Type
        /// </summary>
        /// <param name="interface">@interface</param>
        /// <returns>所有Type</returns>
        public static IEnumerable<Type> AllAssignableFromInterface(Type @interface)
        {
            return AppDomain.CurrentDomain
                            .GetAssemblies()
                            .SelectMany(s => s.GetTypes())
                            .Where(x => x.GetInterfaces()
                                        .Any(y => y.Name == @interface.Name));
        }
    }
}
