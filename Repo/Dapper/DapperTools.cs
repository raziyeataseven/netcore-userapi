using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Dapper;

namespace Repo.Dapper
{
   public interface IDapperTools
   {
      #region generic repo

      T Get<T>(object id) where T : class;

      IEnumerable<T> GetAll<T>() where T : class;

      long Insert<T>(T obj) where T : class;

      long Insert<T>(IEnumerable<T> list) where T : class;

      bool Update<T>(T obj) where T : class;

      bool Update<T>(IEnumerable<T> list) where T : class;

      bool Delete<T>(T obj) where T : class;

      bool Delete<T>(IEnumerable<T> list) where T : class;

      bool DeleteAll<T>() where T : class;

      #endregion generic repo

      #region async generic repo

      Task<T> GetAsync<T>(object id) where T : class;

      Task<IEnumerable<T>> GetAllAsync<T>() where T : class;

      Task<int> InsertAsync<T>(T obj) where T : class;

      Task<int> InsertAsync<T>(IEnumerable<T> list) where T : class;

      Task<bool> UpdateAsync<T>(T obj) where T : class;

      Task<bool> UpdateAsync<T>(IEnumerable<T> list) where T : class;

      Task<bool> DeleteAsync<T>(T obj) where T : class;

      Task<bool> DeleteAsync<T>(IEnumerable<T> list) where T : class;

      Task<bool> DeleteAllAsync<T>() where T : class;

      #endregion async generic repo

      #region db commands

      int Execute(string sql, object param = null);

      IEnumerable<T> Query<T>(string sql, object param = null) where T : class;

      IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map,
          object param = null)
          where TFirst : class
          where TSecond : class
          where TReturn : class;
      IEnumerable<T1> QueryAsync<T1, T2>(string v, object p);
      IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TReturn : class;

      IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TFourth : class
          where TReturn : class;

      IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TFourth : class
          where TFifth : class
          where TReturn : class;

      IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql,
         Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null)
         where TFirst : class
         where TSecond : class
         where TThird : class
         where TFourth : class
         where TFifth : class
         where TSixth : class
         where TReturn : class;


      IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql,
         Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null)
         where TFirst : class
         where TSecond : class
         where TThird : class
         where TFourth : class
         where TFifth : class
         where TSixth : class
         where TSeventh : class
         where TReturn : class;

      IEnumerable<object> Query(string sql, object param = null);

      SqlMapper.GridReader QueryMultiple(string sql, object param = null);


      object ExecuteScalar(string sql, object param = null);
      #endregion db commands

      #region async db commands

      Task<int> ExecuteAsync(string sql, object param = null);

      Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class;

      Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql,
           Func<TFirst, TSecond, TReturn> map, object param = null)
           where TFirst : class
           where TSecond : class
           where TReturn : class;

      Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TReturn : class;

      Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TFourth : class
          where TReturn : class;

      Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql,
          Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null)
          where TFirst : class
          where TSecond : class
          where TThird : class
          where TFourth : class
          where TReturn : class;

      Task<IEnumerable<object>> QueryAsync(string sql, object param = null);

      Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null);

      #endregion async db commands
   }

   public class DapperTools : IDapperTools
   {
      //builder.RegisterAssemblyTypes()
      //       .Where(t => t.Name.EndsWith("Repository"))
      //       .AsImplementedInterfaces()
      //       .InstancePerHttpRequest();

      #region private variables

      private readonly IDapperContext _context;

      #endregion private variables

      #region constructor

      public DapperTools(string connectionString)
      {
         _context = new DapperContext(connectionString);
      }

      public DapperTools(IDapperContext context)
      {
         _context = context;
      }

      #endregion constructor

      #region generic repo

      public T Get<T>(object id) where T : class
      {
         return _context.Connection.Get<T>(id);
      }

      public IEnumerable<T> GetAll<T>() where T : class
      {
         return _context.Connection.GetAll<T>();
      }

      public long Insert<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Insert(obj, transaction, 0);
            return result;
         });
      }

      public long Insert<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Insert(list, transaction);
            return result;
         });
      }

      public bool Update<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Update(obj, transaction);
            return result;
         });
      }

      public bool Update<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Update(list, transaction);
            return result;
         });
      }

      public bool Delete<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Delete(obj, transaction);
            return result;
         });
      }

      public bool Delete<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Delete(list, transaction);
            return result;
         });
      }

      public bool DeleteAll<T>() where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.DeleteAll<T>();
            return result;
         });
      }

      #endregion generic repo

      #region async generic repo

      public Task<T> GetAsync<T>(object id) where T : class
      {
         return _context.Connection.GetAsync<T>(id);
      }

      public Task<IEnumerable<T>> GetAllAsync<T>() where T : class
      {
         return _context.Connection.GetAllAsync<T>();
      }

      public Task<int> InsertAsync<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.InsertAsync(obj, transaction);
            return result;
         });
      }

      public Task<int> InsertAsync<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.InsertAsync(list, transaction);
            return result;
         });
      }

      public Task<bool> UpdateAsync<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.UpdateAsync(obj, transaction);
            return result;
         });
      }

      public Task<bool> UpdateAsync<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.UpdateAsync(list, transaction);
            return result;
         });
      }

      public Task<bool> DeleteAsync<T>(T obj) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.DeleteAsync(obj, transaction);
            return result;
         });
      }

      public Task<bool> DeleteAsync<T>(IEnumerable<T> list) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.DeleteAsync(list, transaction);
            return result;
         });
      }

      public Task<bool> DeleteAllAsync<T>() where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.DeleteAllAsync<T>();
            return result;
         });
      }

      #endregion async generic repo

      #region db commands

      public int Execute(string sql, object param = null)
      {
         return _context.Transaction(transaction => _context.Connection.Execute(sql, param, transaction));
      }

      public IEnumerable<T> Query<T>(string sql, object param = null) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query<T>(sql, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null) where TFirst : class where TSecond : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }

      public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TFifth : class where TSixth : class where TSeventh : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query(sql, map, param, transaction);
            return result;
         });
      }


      public IEnumerable<object> Query(string sql, object param = null)
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.Query<object>(sql, param, transaction);
            return result;
         });
      }

      public SqlMapper.GridReader QueryMultiple(string sql, object param = null)
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryMultiple(sql, param, transaction);
            return result;
         });
      }


      public object ExecuteScalar(string sql, object param = null)
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.ExecuteScalar(sql, param, transaction);
            return result;
         });
      }
      #endregion db commands

      #region async db commands

      public Task<int> ExecuteAsync(string sql, object param = null)
      {
         return _context.Transaction(transaction => _context.Connection.ExecuteAsync(sql, param, transaction));
      }

      public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) where T : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync<T>(sql, param, transaction);
            return result;
         });
      }

      public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null) where TFirst : class where TSecond : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync(sql, map, param, transaction);
            return result;
         });
      }

      public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync(sql, map, param, transaction);
            return result;
         });
      }

      public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync(sql, map, param, transaction);
            return result;
         });
      }

      public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null) where TFirst : class where TSecond : class where TThird : class where TFourth : class where TReturn : class
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync(sql, map, param, transaction);
            return result;
         });
      }

      public Task<IEnumerable<object>> QueryAsync(string sql, object param = null)
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryAsync<object>(sql, param, transaction);
            return result;
         });
      }

      public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null)
      {
         return _context.Transaction(transaction =>
         {
            var result = _context.Connection.QueryMultipleAsync(sql, param, transaction);
            return result;
         });
      }

      public IEnumerable<T1> QueryAsync<T1, T2>(string v, object p)
      {
         throw new NotImplementedException();
      }

      public object Query<T>(object getByKonumId, object p)
      {
         throw new NotImplementedException();
      }

      #endregion async db commands

      //public void Dispose()
      //{
      //   _context.Dispose();
      //}
   }
}
