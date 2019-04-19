using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace Entities.Entity
{
   [Table("users")]
   public class Users
   {
      [Key]
      public int Id { get; set; }
      public string name { get; set; }
      public string surname { get; set; }
      public string email { get; set; }
   }
}
