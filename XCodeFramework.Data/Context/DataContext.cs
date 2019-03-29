using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Context
{
    public class DataContext:ContextBase,IDataContext
    {
        public DataContext()
        {
            _connStr = ConfigurationManager.ConnectionStrings["XCodeWebShop"].ConnectionString;
        }
    }
}
