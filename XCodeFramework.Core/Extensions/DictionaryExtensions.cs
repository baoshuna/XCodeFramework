using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static int GetInt<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValue<TKey, TValue, int>(dictionary, key);
        }

        public static int GetInt<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, int defaultValue)
        {
            return GetValue<TKey, TValue, int>(dictionary, key, defaultValue);
        }

        public static float GetFloat<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValue<TKey, TValue, float>(dictionary, key);
        }

        public static float GetFloat<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, float defaultValue)
        {
            return GetValue<TKey, TValue, float>(dictionary, key, defaultValue);
        }

        public static decimal GetDecimal<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            string value = GetString(dictionary, key);
            if (String.IsNullOrEmpty(value))
            {
                return 0m;
            }
            return Convert.ToDecimal(value, new CultureInfo("en-US"));
        }

        public static decimal GetDecimal<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, decimal defaultValue)
        {
            return GetValue<TKey, TValue, decimal>(dictionary, key, defaultValue);
        }

        public static double GetDouble<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValue<TKey, TValue, double>(dictionary, key);
        }

        public static double GetDouble<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, double defaultValue)
        {
            return GetValue<TKey, TValue, double>(dictionary, key, defaultValue);
        }

        public static bool GetBoolean<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetBoolean(dictionary, key, false);
        }

        public static bool GetBoolean<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, bool defaultValue)
        {
            if (dictionary != null && dictionary.ContainsKey(key) && dictionary[key] != null)
            {
                return dictionary[key].AsBoolean(defaultValue);
            }

            return defaultValue;
        }

        public static string GetString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValue<TKey, TValue, string>(dictionary, key, String.Empty);
        }

        public static string GetString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, string defaultValue)
        {
            return GetValue<TKey, TValue, string>(dictionary, key, defaultValue);
        }

        public static DateTime GetDateTime<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return GetValue<TKey, TValue, DateTime>(dictionary, key);
        }

        public static DateTime GetDateTime<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, DateTime defaultValue)
        {
            return GetValue<TKey, TValue, DateTime>(dictionary, key, defaultValue);
        }

        public static T GetValue<TKey, TValue, T>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.GetValue<TKey, TValue, T>(key, default(T));
        }

        public static T GetValue<TKey, TValue, T>(this IDictionary<TKey, TValue> dictionary, TKey key, T defaultValue)
        {
            if (dictionary != null && dictionary.ContainsKey(key) && dictionary[key] != null)
            {
                return dictionary[key].As<T>(defaultValue);
            }

            return defaultValue;
        }

        public static NameValueCollection ToNameValueCollection<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();

            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                string value = null;
                if (kvp.Value != null)
                {
                    value = kvp.Value.ToString();
                }

                nameValueCollection.Add(kvp.Key.ToString(), value);
            }

            return nameValueCollection;
        }

        public static Dictionary<string, string> ToDictionary(this NameValueCollection nvc)
        {
            var result = new Dictionary<string, string>();
            foreach (string key in nvc.Keys)
            {
                result.Add(key, nvc[key]);
            }

            return result;
        }

        public static Dictionary<string, string> ToDictionary<T>(this T t, bool excludeNullValue = true)
        {
            var dic = new Dictionary<string, string>();

            if (t != null)
            {
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var valueObj = property.GetValue(t, null);
                    if (valueObj == null && excludeNullValue)
                    {
                        continue;
                    }
                    else
                    {
                        var memberInfo = typeof(T).GetProperty(property.Name);
                        var nameAttribute = memberInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();
                        dic.Add(nameAttribute == null ? property.Name.ToLower() : nameAttribute.DisplayName, valueObj.ToString());
                    }
                }

            }

            return dic;
        }
    }
}