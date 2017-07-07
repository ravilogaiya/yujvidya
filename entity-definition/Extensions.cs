using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
namespace yujvidya
{
    public static class Extensions
    {
        public static bool IsSuccessStatusCode(this int? statusCode)
        {
            return statusCode.HasValue && (statusCode.Value >= 200) && (statusCode.Value <= 299);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetRuntimeField(enumVal.ToString());
            var attributes = memInfo.GetCustomAttributes(typeof(T), false).OfType<T>().ToList();

            return (attributes.Count() > 0) ? attributes[0] : null;
        }
    }
}
