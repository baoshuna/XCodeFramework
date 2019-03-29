using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Repository
{
    public interface IBlogSiteRepository<T> : IRepository<T> where T:class
    {

    }
}
