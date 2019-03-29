using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.DbHelper;

namespace XCodeFramework.Data.Repository
{
    public class WebShopRepository<T> : RepositoryBase<T> where T : class
    {
        public WebShopRepository()
        {
            var connStr = ConfigurationManager.ConnectionStrings["XCodeWebShop"].ConnectionString;
            base.conn = ConnectionFactory.CreateConnection(connStr);
        }
    }
}
