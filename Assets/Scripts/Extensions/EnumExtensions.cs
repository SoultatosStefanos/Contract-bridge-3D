using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> AllValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}