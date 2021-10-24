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
        public string Get()
        {
            DBService db = new DBService();
            return JsonConvert.SerializeObject(db.GetUserSteps());
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] UserStepsDto value)
        {
            DBService db = new DBService();
            db.UpdateUserSteps(value.PhoneNumber, Convert.ToInt32(value.steps));
        }
    }
}
