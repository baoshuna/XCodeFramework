using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Core.Log;
using XCodeFramework.Web.Exceptions;

namespace XCodeFramework.Web.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // 自定义的错误过滤器
            // filters.Add(new ExceptionHandlingAttribute());
        }
    }
}