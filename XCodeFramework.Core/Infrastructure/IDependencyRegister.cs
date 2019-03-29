using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace XCodeFramework.Core.Infrastructure
{
    /// <summary>
    /// 为各个层自己的注入方法 提供接口
    /// </summary>
    public interface IDependencyRegister
    {
        void RegisterTypes(IUnityContainer container);
    }
}
