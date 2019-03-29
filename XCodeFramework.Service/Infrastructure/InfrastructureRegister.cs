using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using XCodeFramework.Core.Cache;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Core.Log;

namespace XCodeFramework.Service.Infrastructure
{
    public class InfrastructureRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICacheManager, NullCacheManager>();
            container.RegisterType<ILogManager, NullLogManager>();
        }
    }
}
