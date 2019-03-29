using System;
using System.Collections.Generic;
using System.Reflection;

namespace XCodeFramework.Core.Infrastructure
{
    /// <summary>
    /// 实现此接口的类提供有关类型的信息
    /// 到Nop引擎中的各种服务。
    /// </summary>
    public interface ITypeFinder
    {
        IList<Assembly> GetAssemblies();

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
