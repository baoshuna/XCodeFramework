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
            //ע�⣺Ҫ��web.config���أ���ȡ��ע�������С�
            //ȷ����Unity.Configuration��ӵ�using��䡣
            // container.LoadConfiguration();

            // ��unity.config�ļ������ӵ�������
            string configurationFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unity.config");
            var configurationFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configurationFilePath };
            var configuration = ConfigurationManager.OpenMappedExeConfiguration(configurationFileMap, ConfigurationUserLevel.None);
            var section = (UnityConfigurationSection)configuration.GetSection("unity");
            section.Configure(container);

            // TODO��������ע��������͵�ӳ�䡣
            // container.RegisterInstance(container);//ע���Լ�,���ͷŵ�ʱ���п��ܻ���ִ���

            container.RegisterType<ExceptionHandlingAttribute>();

            //container.RegisterType<IProductRepository, ProductRepository>(new per);

            //���Ͳ�����
            ITypeFinder typeFinder = new WebTypeFinder();
            container.RegisterInstance<ITypeFinder>(typeFinder);

            //ע��Core���е�����
            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            container.RegisterInstance<ApplicationConfig>(config);

            //����ע����
            //�ҵ�����ʵ����IDependencyRegister�ӿڵ���
            //�������е��࣬��ͨ�����䴴��ʵ����ǿת��ע��
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            foreach (Type registerType in registerTypes)
            {
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterTypes(container);
            }
        }
    }
}