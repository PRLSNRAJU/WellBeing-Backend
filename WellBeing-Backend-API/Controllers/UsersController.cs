using Common.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellBeingBackendAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WellBeingBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public string Get()
        {
            DBService db = new DBService();
            return JsonConvert.SerializeObject(db.GetUserSteps());
        }


        // GET api/<UsersController>/5
        [HttpPost]
        public string Post([FromBody] UserStepsDto value)
        {
            DBService db = new DBService();
            db.UpdateUserSteps(value.ID, Convert.ToInt32(value.steps));
            return JsonConvert.SerializeObject(db);
        }
    }
}
