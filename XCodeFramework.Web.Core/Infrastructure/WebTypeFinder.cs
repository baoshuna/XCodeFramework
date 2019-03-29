using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.Infrastructure;

namespace XCodeFramework.Web.Core.Infrastructure
{
    /// <summary>
    /// 提供有关当前Web应用程序中的类型的信息。
    /// 此类可以查看bin文件夹中的所有程序集。
    /// </summary>
    public class WebTypeFinder : AppDomainTypeFinder
    {

        private bool binFolderAssembliesLoaded = false;

        /// <summary>
        /// 获取\ Bin目录的物理磁盘路径
        /// </summary>
        /// <returns>物理路径。 例如: "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                return System.Web.HttpRuntime.BinDirectory;
            }

            //not hosted. For example, run either in unit tests
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!binFolderAssembliesLoaded)
            {
                binFolderAssembliesLoaded = true;
                LoadMatchingAssemblies(GetBinDirectory());
            }

            return base.GetAssemblies();
        }
    }
}
