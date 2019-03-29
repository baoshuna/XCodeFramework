using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Data.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IDbTransaction BeginTransaction();

        IDbConnection GetDbConnection();

        #region 封装的同步方法
        T Get(string id, IDbTransaction transaction = null);

        IEnumerable<T> GetList();

        IEnumerable<T> GetList(object whereConditions, IDbTransaction transaction = null);

        IEnumerable<T> GetList(string conditions, object parameters = null, IDbTransaction transaction = null);

        IEnumerable<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null);

        int RecordCount(string conditions = "", object parameters = null, IDbTransaction transaction = null);

        string Insert(T entity, IDbTransaction transaction = null);

        int Update(T entity, IDbTransaction transaction = null);

        int Delete(T entity, IDbTransaction transaction = null);

        int Delete(string id, IDbTransaction transaction = null);

        int DeleteList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);

        int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);
        #endregion

        # region 封装的异步方法
        Task<T> GetAsync(string id, IDbTransaction transaction = null);

        Task<IEnumerable<T>> GetListAsync();

        Task<IEnumerable<T>> GetListAsync(object whereConditions, IDbTransaction transaction = null);

        Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null, IDbTransaction transaction = null);

        Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null);

        Task<string> InsertAsync(T entity, IDbTransaction transaction = null);

        Task<int> UpdateAsync(T entity, IDbTransaction transaction = null);

        Task<int> DeleteAsync(string id, IDbTransaction transaction = null);

        Task<int> DeleteAsync(T entity, IDbTransaction transaction = null);

        Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null);

        Task<int> RecordCountAsync(string conditions = "", object parameters = null, IDbTransaction transaction = null);


        #endregion

        #region 原本的三个
        void Execute(string sql, object param = null, IDbTransaction transaction = null);

        TEntity Get<TEntity>(string sql, object param = null, IDbTransaction transaction = null);

        IEnumerable<TEntity> Query<TEntity>(string sql, object param = null, IDbTransaction transaction = null);
        #endregion
    }
}
