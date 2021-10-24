using Common.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellBeingBackendAPI.Services;

namespace WellBeingBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public string Get([FromBody] List<UserStepsDto> userSteps)
        {
            DBService db = new DBService();
            return JsonConvert.SerializeObject(db.GetUserSteps());
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<UserDto>(value);
            DBService db = new DBService();
            db.AddUser(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            var user = JsonConvert.DeserializeObject<UserStepsDto>(value);
            DBService db = new DBService();
            db.UpdateUserSteps(user.ID, Convert.ToInt32(user.steps));
        }
    }
}
