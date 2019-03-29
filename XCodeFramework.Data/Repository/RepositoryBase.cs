using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Repository
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected IDbConnection conn;

        /// <summary>事务</summary>
        public IDbTransaction BeginTransaction()
        {
            return conn.BeginTransaction();
        }

        public IDbConnection GetDbConnection()
        {
            return conn;
        }

        #region 拓展的同步CRUD
        public T Get(string id,IDbTransaction transaction = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException($"{nameof(id)} is null");
            }

            return conn.Get<T>(id, transaction);
        }

        public IEnumerable<T> GetList()
        {
            return conn.GetList<T>();
        }

        public IEnumerable<T> GetList(object whereConditions, IDbTransaction transaction = null)
        {
            return conn.GetList<T>(whereConditions, transaction);
        }

        public IEnumerable<T> GetList(string conditions, object parameters = null, IDbTransaction transaction = null)
        {
            if (string.IsNullOrEmpty(conditions))
            {
                throw new ArgumentNullException($"{nameof(conditions)} is null");
            }

            return conn.GetList<T>(conditions, parameters, transaction);
        }

        public IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null)
        {
            return conn.GetListPaged<T>(pageNumber, rowsPerPage, conditions, orderby, parameters, transaction);
        }

        public int RecordCount(string conditions = "", object parameters = null, IDbTransaction transaction = null)
        {
            return conn.RecordCount<T>(conditions, parameters, transaction);
        }

        public string Insert(T entity, IDbTransaction transaction = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }

            return conn.Insert<string, T>(entity, transaction);
        }

        public int Delete(T entity, IDbTransaction transaction = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }

            return conn.Delete(entity, transaction);
        }

        public int Delete(string id, IDbTransaction transaction = null)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException($"{nameof(id)} is null");
            }

            return conn.Delete<T>(id, transaction);
        }

        public int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return conn.DeleteList<T>(whereConditions, transaction, commandTimeout);
        }

        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return conn.DeleteList<T>(conditions, parameters, transaction, commandTimeout);
        }

        public int Update(T entity, IDbTransaction transaction = null)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} is null");
            }

            return conn.Update(entity, transaction);
        }
        #endregion

        #region 拓展的异步CRUD
        public async Task<T> GetAsync(string id, IDbTransaction transaction = null)
        {
            return await conn.GetAsync<T>(id, transaction);
        }

        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await conn.GetListAsync<T>();
        }

        public async Task<IEnumerable<T>> GetListAsync(object whereConditions, IDbTransaction transaction = null)
        {
            return await conn.GetListAsync<T>(whereConditions, transaction);
        }

        public async Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null, IDbTransaction transaction = null)
        {
            return await conn.GetListAsync<T>(conditions, parameters, transaction);
        }

        public async Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null)
        {
            return await conn.GetListPagedAsync<T>(pageNumber, rowsPerPage, conditions, orderby, parameters, transaction);
        }

        public async Task<string> InsertAsync(T entity, IDbTransaction transaction = null)
        {
            return await conn.InsertAsync<string, T>(entity, transaction);
        }

        public async Task<int> UpdateAsync(T entity, IDbTransaction transaction = null)
        {
            return await conn.UpdateAsync(entity, transaction);
        }

        public async Task<int> DeleteAsync(string id, IDbTransaction transaction = null)
        {
            return await conn.DeleteAsync(id, transaction);
        }

        public async Task<int> DeleteAsync(T entity, IDbTransaction transaction = null)
        {
            return await conn.DeleteAsync(entity, transaction);
        }

        public async Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await conn.DeleteListAsync<T>(whereConditions, transaction, commandTimeout);
        }

        public async Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return await conn.DeleteListAsync<T>(conditions, parameters, transaction, commandTimeout);
        }

        public async Task<int> RecordCountAsync(string conditions = "", object parameters = null, IDbTransaction transaction = null)
        {
            return await conn.RecordCountAsync<T>(conditions, parameters);
        }
        #endregion

        #region 源生的三个
        public TEntity Get<TEntity>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return SqlMapper.Query<TEntity>(conn, sql, param, transaction).FirstOrDefault();
        }

        public IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbTransaction transaction = null)
        {
            return SqlMapper.Query<TEntity>(conn, sql, param, transaction);
        }

        public void Execute(string sql, object pararm = null, IDbTransaction transaction = null)
        {
            SqlMapper.Execute(conn, sql, pararm, transaction);
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    conn?.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        //~RepositoryBase()
        //{
        //    // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //    Dispose(false);
        //}

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
