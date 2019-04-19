using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repo;
using Entities;

namespace UserApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ValuesController : ControllerBase
   {
      public Repo.IRepository.IUsersRepository _usersRepository;
      public ValuesController()
      {
         _usersRepository = new Repo.Repository.UsersRepository();
      }
      // GET api/values
      [HttpGet]
      public ActionResult<IEnumerable<Entities.Entity.Users>> Get()
      {
         return _usersRepository.GetAll();          
      }

      // GET api/values/5
      [HttpGet("{id}")]
      public ActionResult<Entities.Entity.Users> Get(int id)
      {
         return _usersRepository.Get(id);
      }

      // POST api/values
      [HttpPost]
      public void Post([FromBody] string value)
      {
      }

      // PUT api/values/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody] string value)
      {
      }

      // DELETE api/values/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
   }
}
