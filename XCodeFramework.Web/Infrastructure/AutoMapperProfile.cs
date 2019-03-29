using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XCodeFramework.Web.Models.Student;

namespace XCodeFramework.Web.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        // 自动mapping，比如说User类自动映射到UserModel类(视图模型)
        private readonly string MvcViewModelClassSuffixName = "Model";

        public AutoMapperProfile()
        {
            // this.CreateMap<Student, StudentModel>();
            var modelTypes = this.GetType().Assembly.GetTypes().Where(t => t.Name.EndsWith(MvcViewModelClassSuffixName));

            var domainTypes = typeof(XCodeFramework.Core.Domain.Common.BaseEntity).Assembly.GetTypes();

            foreach (Type modelType in modelTypes)
            {
                var modelTypeRelateDomainType = domainTypes.SingleOrDefault(domainType => domainType.Name + MvcViewModelClassSuffixName == modelType.Name);
                if (modelTypeRelateDomainType != null)
                {
                    this.CreateMap(modelType, modelTypeRelateDomainType);
                    this.CreateMap(modelTypeRelateDomainType, modelType).MaxDepth(10);
                }
            }
        }
    }
}