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
        /// Ӧ�ó�������ʱ����Unity��
        /// </summary>
        public static void Start() 
        {
            var container = UnityConfig.GetConfiguredContainer();
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: ���Ҫʹ��PerRequestLifetimeManager����ȡ��ע��
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// �ر�Ӧ�ó���ʱ����Unity������
        /// </summary>
        public static void Shutdown()
        {
            var container = UnityConfig.GetConfiguredContainer();
            container.Dispose();
        }
    }
}