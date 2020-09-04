using System.Collections.Generic;

namespace Spinx.Web.Modules.Core.Extensions
{
    public static class ListHelper
    {
        public static void AddIfNotExists<T>(this List<T> dict, T value)
        {
            if (!dict.Contains(value))
                dict.Add(value);
        }
    }
}