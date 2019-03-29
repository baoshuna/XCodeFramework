using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XCodeFramework.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Target attribute should not be multiple
        /// </summary>
        /// <typeparam name="T">Target attribute type</typeparam>
        /// <param name="value">Enum value</param>
        /// <returns>Return <c>null</c> if attribute is not defined</returns>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            return Attribute.GetCustomAttribute(field, typeof(T)) as T;
        }

        /// <summary>
        /// Get Description for enum value
        /// </summary>
        public static string ToDescription(this Enum value)
        {
            var desc = value.GetAttribute<DescriptionAttribute>();
            return desc?.Description ?? "";
        }

        /// <summary>
        /// Gets a dictionay with attribute text as key, enum as value. Attribute text property value should be unique
        /// </summary>
        /// <typeparam name="TAttribute">Target attribute to get text from</typeparam>
        /// <param name="enumType">Type of enum</param>
        /// <param name="textPropertyName">Text property name of target attribute</param>
        /// <returns>Dictionay with attribute text as key, enum as value</returns>
        public static Dictionary<string, Enum> ToEnumDictionary<TAttribute>(this Type enumType, string textPropertyName)
            where TAttribute : Attribute
        {
            var propinfo = typeof(TAttribute).GetProperty(textPropertyName);
            if (propinfo == null) return default(Dictionary<string, Enum>);

            var dict = new Dictionary<string, Enum>();
            foreach (Enum value in enumType.GetEnumValues())
            {
                var attr = value.GetAttribute<TAttribute>();
                if (attr == null) continue;
                var text = propinfo.GetValue(attr);
                if (text == null) continue;
                dict.Add(text.ToString(), value);
            }
            return dict;
        }

        /// <summary>
        /// Gets a dictionay with description text as key, enum as value. Description should be unique
        /// </summary>
        /// <returns>Dictionay with description text as key, enum as value</returns>
        public static Dictionary<string, Enum> ToDescriptionDictionary(this Type enumType)
        {
            return enumType.ToEnumDictionary<DescriptionAttribute>("Description");
        }
    }
}
