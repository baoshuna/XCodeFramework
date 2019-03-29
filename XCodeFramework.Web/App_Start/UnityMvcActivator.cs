using System.Linq;
using System.Web.Mvc;

using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(XCodeFramework.Web.UnityMvcActivator), nameof(XCodeFramework.Web.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(XCodeFramework.Web.UnityMvcActivator), nameof(XCodeFramework.Web.UnityMvcActivator.Shutdown))]

namespace XCodeFramework.Web
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// 应用程序启动时集成Unity。
        /// </summary>
        public static void Start() 
        {
            var container = UnityConfig.GetConfiguredContainer();
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: 如果要使用PerRequestLifetimeManager，请取消注释
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// 关闭应用程序时处置Unity容器。
        /// </summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}