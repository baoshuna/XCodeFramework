using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XCodeFramework.Web.Properties;

namespace XCodeFramework.Web.Infrastructure
{
    /// <summary>
    /// 用自己资源的【displayname】替换mvc自带的特性【displayname】
    /// </summary>
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            if (string.IsNullOrWhiteSpace(modelMetadata.DisplayName) && containerType != null && containerType.Assembly == typeof(Resources).Assembly)
            {
                string propertyDisplayName = Resources.ResourceManager.GetString(containerType.Name.Replace(".", string.Empty) + propertyName + nameof(modelMetadata.DisplayName));

                if (!string.IsNullOrWhiteSpace(propertyDisplayName))
                {
                    modelMetadata.DisplayName = propertyDisplayName;
                }
            }

            return modelMetadata;
        }
    }
}