using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.ComponentModel;

namespace XCodeFramework.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T each in enumerable)
            {
                action(each);
            }
        }

        public static void Each<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int index = 0;
            foreach (T each in enumerable)
            {
                action(each, index++);
            }
        }

        public static IEnumerable<T> Distinct<T, TIdentity>(this IEnumerable<T> source, Func<T, TIdentity> identitySelector)
        {
            return source.Distinct(new DistinctFuncEqualityComparer<T, TIdentity>(identitySelector));
        }

        public static string GetEnumDesc(Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DisplayAttribute)attrs[0]).Name;
            }

            return en.ToString();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> items)
        {
            var table = new DataTable(typeof(T).Name);

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var memberInfo = typeof(T).GetProperty(propertyName);
                var descriptionAttribute = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();

                if (descriptionAttribute != null)
                {
                    propertyName = descriptionAttribute.DisplayName;
                }

                table.Columns.Add(propertyName, property.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[properties.Length];
                for (var i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(item, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }

        private class DistinctFuncEqualityComparer<T, TIdentity> : IEqualityComparer<T>
        {
            private readonly Func<T, TIdentity> _identitySelector;

            public DistinctFuncEqualityComparer(Func<T, TIdentity> identitySelector)
            {
                this._identitySelector = identitySelector;
            }

            public bool Equals(T x, T y)
            {
                return Equals(_identitySelector(x), _identitySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return _identitySelector(obj).GetHashCode();
            }
        }
    }
}