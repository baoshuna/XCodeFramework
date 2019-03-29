using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Extensions
{
    public static class MoneyExtensions
    {
        /// <summary>
        /// For example, if the currency is 'EUR', then it will return '€'.
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToCurrencySymbol(this string currency)
        {
            string currencySymbol;

            currency = currency.AsString().ToUpper();
            switch (currency)
            {
                case "EUR":
                    currencySymbol = "€";
                    break;
                case "GBP":
                    currencySymbol = "£";
                    break;
                case "USD":
                    currencySymbol = "$";
                    break;
                default:
                    currencySymbol = "€";
                    break;
            }

            return currencySymbol;
        }

        /// <summary>
        /// For example, if the cents is '1250', then it will return '12,50'.
        /// </summary>
        /// <param name="cents"></param>
        /// <returns></returns>
        public static string ToDecimalString(this long cents)
        {
            return ((double) cents/100).ToString("f2");
        }

        public static string ToDecimalString(this long cents, CultureInfo culture)
        {
            return ((double)cents / 100).ToString("f2", culture);
        }

        /// <summary>
        /// For example, if the cents is '1250' and currency is 'EUR', then it will return '€ 12,50'.
        /// </summary>
        /// <param name="cents"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string ToDecimalStringWithCurrencySymbol(this long cents, string currency)
        {
            string currencySymbol = currency.ToCurrencySymbol();
            return String.Format("{0} {1}", currencySymbol, cents.ToDecimalString());
        }

        public static string ToUSFormatString(this decimal money)
        {
            return money.ToString("N", new CultureInfo("en-US"));
        }

        public static string ToUSFormatString(this double money)
        {
            return money.ToString("N", new CultureInfo("en-US"));
        }
    }
}