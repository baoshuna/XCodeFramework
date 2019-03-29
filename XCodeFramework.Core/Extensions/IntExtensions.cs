using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Extensions
{
    public static class IntExtensions
    {
        public static void Repeat(this int times, Action action)
        {
            for (int index = 0; index < times; index++)
            {
                action();
            }
        }

        public static void Repeat(this int times, Action<int> action)
        {
            for (int index = 0; index < times; index++)
            {
                action(index);
            }
        }

        public static bool Repeat(this int times, Func<bool> func)
        {
            for (int index = 0; index < times; index++)
            {
                if (!func())
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Repeat(this int times, Func<int, bool> func)
        {
            for (int index = 0; index < times; index++)
            {
                if (!func(index))
                {
                    return false;
                }
            }

            return true;
        }
    }
}