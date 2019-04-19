using Entities.Entity;
using NUnit.Framework;
using Repo.IRepository;
using Repo.Repository;
using System.Collections.Generic;

namespace NUnitTestRepo
{ 
   public class UsersTest
   {
      private static IUsersRepository _usersRepository;

      [SetUp]
      public void Setup()
      {
         _usersRepository = new UsersRepository();
      }

      [Test]
      public void get_all_users()
      {
         List<Users> users = _usersRepository.GetAll();
         Assert.True(users.Count > 0, "get_all_users test method failed. There is no user object!");
         Assert.Pass();
      }

      [Test]
      public void get_all_user_by_id()
      {
         Users user = _usersRepository.Get(1);
         Assert.NotNull(user, "get_all_user_by_id test method failed. User object shouldn't be null!");
         Assert.Pass();
      }
   }
}
