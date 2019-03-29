using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace XCodeFramework.Core.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Include a script in a partial view
        /// </summary>
        /// <example>
        /// @Html.IncludeScriptInPartialView(
        ///     @<script type="text/javascript">
        ///         $(document).ready(function() {
        ///         });
        ///     </script>
        /// )
        /// </example>
        /// <param name="htmlHelper"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString IncludeScriptInPartialView(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_IncludeScriptInPartialView_" + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// Render scripts from partial views
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlString RenderScriptsFromPartialViews(this HtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_IncludeScriptInPartialView_"))
                {
                    Func<object, HelperResult> template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        htmlHelper.ViewContext.Writer.Write(template(null));
                    }
                }
            }

            return MvcHtmlString.Empty;
        }
    }
}
