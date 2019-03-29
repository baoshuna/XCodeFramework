using System;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using XCodeFramework.Core.Config;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Web.Core.Infrastructure;

namespace XCodeFramework.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //注意：要从web.config加载，请取消注释以下行。
            //确保将Unity.Configuration添加到using语句。
            // container.LoadConfiguration();

            // TODO：在这里注册你的类型的映射。
            container.RegisterInstance<IUnityContainer>(container);//注入自己！！？？
            

            //类型查找器
            ITypeFinder typeFinder = new WebTypeFinder();
            container.RegisterInstance<ITypeFinder>(typeFinder);

            //注入Core层中的配置
            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            container.RegisterInstance<ApplicationConfig>(config);

            //各层注册器
            //找到所有实现了IDependencyRegister接口的类
            //遍历所有的类，并通过反射创建实例，强转，注册
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            foreach (Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }
        }
    }
}