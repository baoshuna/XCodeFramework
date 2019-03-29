using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity;
using XCodeFramework.Core.Cache;
using XCodeFramework.Core.Domain.Common;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Data.Repository;
using XCodeFramework.Data.Context;
using Unity.Registration;

namespace XCodeFramework.Data.Infrastructure
{
    public class RepositoryRegister: IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // container.RegisterType<IRepository<Student>, WebShopRepository<Student>>();

            container.RegisterType(typeof(IRepository<>), typeof(WebShopRepository<>));
            container.RegisterType(typeof(IBlogSiteRepository<>), typeof(BlogSiteRepository<>));
            container.RegisterType<IDataContext, DataContext>();
        }
    }
}