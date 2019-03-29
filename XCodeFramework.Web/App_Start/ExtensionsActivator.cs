using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Mvc;
using XCodeFramework.Web.Infrastructure;
using XCodeFramework.Web.Validator;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(XCodeFramework.Web.App_Start.ExtensionsActivator), "Start")]
namespace XCodeFramework.Web.App_Start
{
    public static class ExtensionsActivator
    {
        public static void Start()
        {
            // 关闭MVC自带的验证器
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            // 添加自己的验证器
            //UnityValidatorFactory factory = new UnityValidatorFactory(UnityConfig.GetConfiguredContainer());
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider());

            //用自己资源的displayname替换mvc自带的特性displayname
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();

            //var clientDataTypeValidator = ModelValidatorProviders.Providers.OfType<ClientDataTypeModelValidatorProvider>().FirstOrDefault();
            //if (clientDataTypeValidator != null)
            //{
            //    ModelValidatorProviders.Providers.Remove(clientDataTypeValidator);
            //}
            //ModelValidatorProviders.Providers.Add(new CustomClientDataTypeModelValidatorProvider());
        }
    }
}