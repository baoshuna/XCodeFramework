using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace XCodeFramework.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Replace multiple whitespaces with single whitespace
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReduceWhitespace(this string source)
        {
            var newString = new StringBuilder();

            bool previousIsWhitespace = false;
            for (int i = 0; i < source.Length; i++)
            {
                if (Char.IsWhiteSpace(source[i]))
                {
                    if (previousIsWhitespace)
                    {
                        continue;
                    }

                    previousIsWhitespace = true;
                }
                else
                {
                    previousIsWhitespace = false;
                }

                newString.Append(source[i]);
            }

            return newString.ToString();
        }

        /// <summary>
        /// Returns SEO url keyword
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToSeoUrlKeyword(this string source)
        {
            if (String.IsNullOrEmpty(source))
            {
                return source;
            }

            var seoUrlKeyword = source.ToLower().Trim();

            seoUrlKeyword = Regex.Replace(seoUrlKeyword, @"[^A-Za-z0-9_~]+", "-").Trim('-');

            return seoUrlKeyword;
        }

        private static readonly Regex FriendlyNameExpression = new Regex(@"(?!^)(?=[A-Z])");

        public static DateTime AsDateTime(this string value, string format)
        {
            return AsDateTime(value, format, default(DateTime));
        }

        public static DateTime AsDateTime(this string value, string format, DateTime defaultValue)
        {
            DateTime convertedDateTime = defaultValue;

            if (!String.IsNullOrEmpty(value))
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    convertedDateTime = dateTime;
                }
            }

            return convertedDateTime;
        }

        public static string[] SplitRemoveEmptyEntries(this string source)
        {
            return SplitRemoveEmptyEntries(source, ',');
        }

        public static string[] SplitRemoveEmptyEntries(this string source, char separator)
        {
            return SplitRemoveEmptyEntries(source, new char[] { separator });
        }

        public static string[] SplitRemoveEmptyEntries(this string source, char[] separator)
        {
            if (String.IsNullOrEmpty(source))
            {
                return new string[] { };
            }

            return source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitRemoveEmptyEntries(this string source, string separator)
        {
            return SplitRemoveEmptyEntries(source, new string[] { separator });
        }

        public static string[] SplitRemoveEmptyEntries(this string source, string[] separator)
        {
            if (String.IsNullOrEmpty(source))
            {
                return new string[] { };
            }

            return source.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string[] SplitRemoveEmptyEntries(this string source, string separator, int count)
        {
            return SplitRemoveEmptyEntries(source, new string[] { separator }, count);
        }

        public static string[] SplitRemoveEmptyEntries(this string source, string[] separator, int count)
        {
            if (String.IsNullOrEmpty(source))
            {
                return new string[] { };
            }

            return source.Split(separator, count, StringSplitOptions.RemoveEmptyEntries);
        }

        public static string UrlEncode(this string source)
        {
            return HttpUtility.UrlEncode(source);
        }

        public static string UrlDecode(this string source)
        {
            return HttpUtility.UrlDecode(source);
        }

        public static string HtmlEncode(this string source)
        {
            return HttpUtility.HtmlEncode(source);
        }

        public static string HtmlDecode(this string source)
        {
            return HttpUtility.HtmlDecode(source);
        }

        public static string FriendlyName(this string source)
        {
            return FriendlyNameExpression.Replace(source, " ");
        }

        public static string RemoveWhiteSpaces(this string source)
        {
            return source
                .Replace(" ", "")
                .Replace("\n", "")
                .Replace("\r", "");
        }

        public static string RemoveTags(this string html, bool htmlDecode = false)
        {
            if (String.IsNullOrEmpty(html))
            {
                return String.Empty;
            }

            char[] result = new char[html.Length];

            int cursor = 0;
            bool inside = false;
            for (int i = 0; i < html.Length; i++)
            {
                char current = html[i];

                switch (current)
                {
                    case '<':
                        inside = true;
                        continue;
                    case '>':
                        inside = false;
                        continue;
                }

                if (!inside)
                {
                    result[cursor++] = current;
                }
            }

            string stringResult = new string(result, 0, cursor);

            if (htmlDecode)
            {
                stringResult = HttpUtility.HtmlDecode(stringResult);
            }

            return stringResult;
        }

        public static string HighLightByHtmlNode(this string originalText, string searchText, string startNode, string endNode)
        {
            if (String.IsNullOrWhiteSpace(originalText))
            {
                return originalText;
            }

            int indexOfStartNode = originalText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);
            if (indexOfStartNode < 0)
            {
                return originalText;
            }

            int indexOfEndNode = indexOfStartNode + searchText.Length;

            return originalText.Insert(indexOfEndNode, endNode).Insert(indexOfStartNode, startNode);
        }

        public static string GetLast(this string source, int length)
        {
            if (String.IsNullOrEmpty(source) || length >= source.Length)
            {
                return source;
            }
            return source.Substring(source.Length - length);
        }

        public static string GetFirst(this string source, int length)
        {
            if (String.IsNullOrEmpty(source) || length >= source.Length)
            {
                return source;
            }
            return source.Substring(0, length);
        }

        /// <summary>
        /// Convert the camel string to first string lower. CustomerID to customerID. XMLHeader to xmlHeader.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>

        public static string LowerFirstCamelWord(this string source)
        {
            var reversedChars = source.Select(c => char.IsLetter(c) ? (char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c)) : c).ToArray();
            var reversedCase = new string(reversedChars);
            var lowerCaseStr = source.ToLower();

            if (reversedCase.Equals(lowerCaseStr))
            {
                return reversedCase;
            }
            string newStr = string.Empty;
            int index = 0;
            var lowerCaseChars = lowerCaseStr.ToCharArray();
            foreach (var reversedChar in reversedChars)
            {
                if (lowerCaseChars.Any(c => c.Equals(reversedChar)))
                {
                    newStr += reversedChar;
                    index++;
                }
                else
                {
                    break;
                }
            }

            if (index < 2)
            {
                newStr += source.Substring(index);
            }
            else
            {
                newStr = newStr.Substring(0, newStr.Length - 1) + source.Substring(index - 1);
            }
            return newStr;
        }

        public static string ClearHtml(this string html)
        {
            if (String.IsNullOrWhiteSpace(html))
            {
                return html;
            }

            string result = Regex.Replace(html, "<[^>]+>", "");
            result = Regex.Replace(result, "&[^;]+;", "");

            return result;
        }

        public static string Summary(this string source, int characterCount, string ellipsis = "...")
        {
            if (String.IsNullOrWhiteSpace(source))
            {
                return String.Empty;
            }

            string summary = source.RemoveTags();
            if (summary.Length > characterCount)
            {
                while (characterCount > 0 && Char.IsLetterOrDigit(summary[characterCount]))
                {
                    characterCount--;
                }

                if (characterCount > 0)
                {
                    summary = summary.Substring(0, characterCount) + " " + ellipsis;
                }
                else
                {
                    summary = String.Empty;
                }
            }

            return summary;
        }
    }
}