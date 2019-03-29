using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Context
{
    public interface IDataContext
    {
        void Execute(string sql, object param = null);

        T Get<T>(string sql, object param = null);

        IEnumerable<T> Query<T>(string sql, object param = null);
    }
}
