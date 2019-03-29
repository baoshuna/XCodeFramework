using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Extensions
{
    public static class ObjectExtensions
    {
        private static readonly string[] Booleans = new string[] { "true", "yes", "on", "1" };

        public static int AsInt(this object value, IFormatProvider provider = null)
        {
            return As<int>(value, provider);
        }

        public static int AsInt(this object value, int defaultValue, IFormatProvider provider = null)
        {
            return As<int>(value, defaultValue, provider);
        }

        public static float AsFloat(this object value, IFormatProvider provider = null)
        {
            return As<float>(value, provider);
        }

        public static float AsFloat(this object value, float defaultValue, IFormatProvider provider = null)
        {
            return As<float>(value, defaultValue, provider);
        }

        public static decimal AsDecimal(this object value, IFormatProvider provider = null)
        {
            return As<decimal>(value, provider);
        }

        public static decimal AsDecimal(this object value, decimal defaultValue, IFormatProvider provider = null)
        {
            return As<decimal>(value, defaultValue, provider);
        }

        public static double AsDouble(this object value, IFormatProvider provider = null)
        {
            return As<double>(value, provider);
        }

        public static double AsDouble(this object value, double defaultValue, IFormatProvider provider = null)
        {
            return As<double>(value, defaultValue, provider);
        }

        public static long AsLong(this object value, IFormatProvider provider = null)
        {
            return As<long>(value, provider);
        }

        public static long AsDouble(this object value, long defaultValue, IFormatProvider provider = null)
        {
            return As<long>(value, defaultValue, provider);
        }

        public static bool AsBoolean(this object value, IFormatProvider provider = null)
        {
            return AsBoolean(value, default(bool), provider);
        }

        public static bool AsBoolean(this object value, bool defaultValue, IFormatProvider provider = null)
        {
            string valueAsString = value.AsString(provider);

            if (!String.IsNullOrEmpty(valueAsString))
            {
                return Booleans.Contains(valueAsString, StringComparer.OrdinalIgnoreCase);
            }

            return defaultValue;
        }

        public static string AsString(this object value, IFormatProvider provider = null)
        {
            return As<string>(value, String.Empty, provider);
        }

        public static string AsString(this object value, string defaultValue, IFormatProvider provider = null)
        {
            return As<string>(value, defaultValue, provider);
        }

        public static DateTime AsDateTime(this object value, IFormatProvider provider = null)
        {
            return As<DateTime>(value, provider);
        }

        public static DateTime AsDateTime(this object value, DateTime defaultValue, IFormatProvider provider = null)
        {
            return As<DateTime>(value, defaultValue, provider);
        }

        public static T As<T>(this object value, IFormatProvider provider = null)
        {
            return As<T>(value, default(T), provider);
        }

        public static T As<T>(this object value, T defaultValue, IFormatProvider provider = null)
        {
            T convertedValue = defaultValue;

            if (value != null && value != DBNull.Value && value is IConvertible)
            {
                try
                {
                    convertedValue = (T) value;
                }
                catch
                {
                    try
                    {
                        if (provider == null)
                        {
                            provider = new CultureInfo("nl-NL");
                        }

                        if (provider == null)
                        {
                            convertedValue = (T) Convert.ChangeType(value, typeof(T));
                        }
                        else
                        {
                            convertedValue = (T) Convert.ChangeType(value, typeof(T), provider);                            
                        }
                    }
                    catch
                    {
                    }
                }
            }

            return convertedValue;
        }

        public static string ParseToJson(this object obj, bool useCamelCasePropertyNaming = true)
        {
            var jsonSerializerSettings = useCamelCasePropertyNaming ? new Newtonsoft.Json.JsonSerializerSettings()
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                Converters = new List<Newtonsoft.Json.JsonConverter>()
                {
                    new StringEnumConverter()
                }
            }
            : null;

            return obj == null ? string.Empty : Newtonsoft.Json.JsonConvert.SerializeObject(obj, jsonSerializerSettings);
        }
    }
}