using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiSample.Filter;

namespace WebApiSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            // 配置注册自己的管道hanglder
            // config.Filters.Add(new MyExceptionFilterAttribute());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
