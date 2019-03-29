using Microsoft.Practices.Unity.Configuration;
using System;
using System.Configuration;
using Unity;
using XCodeFramework.Core.Config;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Service;
using XCodeFramework.Service.Services.Students;
using XCodeFramework.Web.Core.Infrastructure;
using XCodeFramework.Web.Exceptions;

namespace XCodeFramework.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            RegisterTypes(ServiceContainer.Current);

            return ServiceContainer.Current;
        }
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            //注意：要从web.config加载，请取消注释以下行。
            //确保将Unity.Configuration添加到using语句。
            // container.LoadConfiguration();

            // 将unity.config文件，增加到容器中
            string configurationFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unity.config");
            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilePath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            var section = (UnityConfigurationSection)configuration.GetSection("unity");
            section.Configure(container);

            // TODO：在这里注册你的类型的映射。
            // container.RegisterInstance(container);//注入自己,在释放的时候有可能会出现错误

            container.RegisterType<ExceptionHandlingAttribute>();

            //container.RegisterType<IProductRepository, ProductRepository>(new per);

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