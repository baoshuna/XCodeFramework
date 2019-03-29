using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;
using XCodeFramework.Core.Infrastructure;

namespace XCodeFramework.Web.Validator
{
    public class UnityValidatorFactory : ValidatorFactoryBase
    {
        private readonly IUnityContainer container;

        public UnityValidatorFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            // return container.TryResolve(validatorType, validatorType.GetGenericArguments().First().FullName) as IValidator;

            IValidator validator = null;
            try
            {
                validator = container.Resolve(validatorType, validatorType.GetGenericArguments().First().FullName) as IValidator;
            }
            catch
            {
                validator = null;
            }
            return validator;
        }
    }
}