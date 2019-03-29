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
            //ע�⣺Ҫ��web.config���أ���ȡ��ע�������С�
            //ȷ����Unity.Configuration��ӵ�using��䡣
            // container.LoadConfiguration();

            // TODO��������ע��������͵�ӳ�䡣
            container.RegisterInstance<IUnityContainer>(container);//ע���Լ���������
            

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