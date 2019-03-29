using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.DbHelper;

namespace XCodeFramework.Data.Repository
{
    public class BlogSiteRepository<T> : RepositoryBase<T> , IBlogSiteRepository<T> where T : class
    {
        public BlogSiteRepository()
        {
            var connStr = ConfigurationManager.ConnectionStrings["BlogSite"].ConnectionString;
            base.conn = ConnectionFactory.CreateConnection(connStr);
        }
    }
}
