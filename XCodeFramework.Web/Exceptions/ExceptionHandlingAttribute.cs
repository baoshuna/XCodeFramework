using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Core.Log;

namespace XCodeFramework.Web.Exceptions
{
    public class ExceptionHandlingAttribute : HandleErrorAttribute
    {
        private readonly ILogManager logger = ServiceContainer.Resolve<ILogManager>();

        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            var ex = filterContext.Exception;
            logger.Error("发现未处理的异常:", ex);

            filterContext.Result = new RedirectResult("~/ErrorHtml/Error.html");
        }
    }
}