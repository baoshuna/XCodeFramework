using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace XCodeFramework.Core.Extensions
{
    public static class WebPageBaseExtension
    {
        public static HelperResult RenderSection(this WebPageBase webPage, string name, Func<dynamic, HelperResult> defaultContents)
        {
            if (webPage.IsSectionDefined(name))
            {
                return webPage.RenderSection(name);
            }
            return defaultContents(null);
        }
    }
}
