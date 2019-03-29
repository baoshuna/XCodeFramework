using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Context
{
    public class ContextBase
    {
        protected string _connStr;

        public T Get<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                return Dapper.SqlMapper.Query<T>(conn, sql, param).FirstOrDefault();
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                return Dapper.SqlMapper.Query<T>(conn, sql, param);
            }
        }

        public void Execute(string sql, object pararm = null)
        {
            using (var conn = new SqlConnection(_connStr))
            {
                Dapper.SqlMapper.Execute(conn, sql, pararm);
            }
        }
    }
}
