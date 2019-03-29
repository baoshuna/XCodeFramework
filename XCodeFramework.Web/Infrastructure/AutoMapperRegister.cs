using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;
using Unity.RegistrationByConvention;
using XCodeFramework.Core.Cache;
using XCodeFramework.Core.Infrastructure;

namespace XCodeFramework.Web.Infrastructure
{
    public class AutoMapperRegister: IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // 找到所有的类型，实现了【Profile】接口的
            var profileTypes = this.GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));

            // 反射方法创建实例
            var profileInstances = profileTypes.Select(t => (Profile)Activator.CreateInstance(t));

            // 配置
            var config = new MapperConfiguration(cfg => { profileInstances.ToList().ForEach(p => cfg.AddProfile(p)); });

            // 注入配置和IMapper的 单例实例
            container.RegisterInstance<MapperConfiguration>(config); //IConfigurationProvider  

            container.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}