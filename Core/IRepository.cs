using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
   public interface IRepository<T>
   {
      long Insert(T obj);
      T Get(int Id);      
      List<T> GetAll();
      bool Update(T obj);
      bool Delete(int ID);

   }
}
