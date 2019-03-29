using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;
using Unity.Registration;
using XCodeFramework.Core.Infrastructure;
using XCodeFramework.Service.Services.Students;

namespace XCodeFramework.Service.Infrastructure
{
    public class ServiceRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            //container.RegisterType<IStudentService, StudentService>(new)

        }
    }
}
