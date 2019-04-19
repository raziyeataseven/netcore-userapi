using Entities.Entity;
using Repo.Dapper;
using Repo.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repo.Repository
{

   public class UsersRepository : IUsersRepository
   {
      private IDapperTools _dapper; 

      public UsersRepository()
      {
         _dapper = new DapperTools(ToDoContext.connectionString);
      }

      public bool Delete(int ID)
      {
         try
         {
            var user = new Users { Id = ID };
            return _dapper.Delete<Users>(user);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public Users Get(int Id)
      {
         try
         {
            return _dapper.Get<Users>(Id);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public List<Users> GetAll()
      {
         try
         {
            return _dapper.GetAll<Users>().ToList();
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public long Insert(Users obj)
      {
         try
         {
            return _dapper.Insert<Users>(obj);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      public bool Update(Users obj)
      {
         try
         {
            return _dapper.Update<Users>(obj);
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
   }
}
