using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;
using Unity;
using Unity.Lifetime;
using XCodeFramework.Core.Infrastructure;

namespace XCodeFramework.Web.Validator
{
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                var displayColumnAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();

                if (displayColumnAttribute != null)
                {
                    return displayColumnAttribute.Name;
                }

                var displayNameAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (displayNameAttribute != null)
                {
                    return displayNameAttribute.DisplayName;
                }

                var resourceManager = new ResourceManager(typeof(XCodeFramework.Web.Properties.Resources));

                return resourceManager.GetString(type.Name + memberInfo.Name + nameof(displayNameAttribute.DisplayName));
            };

            //FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();

            // 查到所有验证器程序集类型
            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            foreach (Type instanceType in validatorTypes)
            {
                // 生命周期注入单例,用同一个容器
                container.RegisterType(typeof(IValidator<>), instanceType, instanceType.BaseType.GetGenericArguments().First().FullName, new ContainerControlledLifetimeManager());
            }
        }
    }
}