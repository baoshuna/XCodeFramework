using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.DbHelper
{
    public class ConnectionFactory
    {
        // 这里可以实现获取多数据库的连接，这里没有做，默认获取的sql server
        // 如果要实现多数据库连接，方法多加一个数据库连接的参数，进行判断
        public static IDbConnection CreateConnection(string connStr)
        {
            #region CallContext
            //var connection = CallContext.GetData("dbcontext") as SqlConnection;
            //if (connection == null)
            //{
            //    connection = new SqlConnection(connStr);
            //    CallContext.SetData("dbcontext", connection);
            //}
            #endregion

            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException(nameof(connStr));
            }
            var connection = new SqlConnection(connStr);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection ;
        }
    }
}
