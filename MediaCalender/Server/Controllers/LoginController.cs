using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaCalender.Shared.Containers;
using MediaCalender.Server.CsClasses;



namespace MediaCalender.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public Database database;
        public LoginSystemOld loginSystem;
        public LoginController(Database database, LoginSystemOld loginSystem)
        {
            this.database = database;
            this.loginSystem = loginSystem;
        }
        
        #region LoginSystem
        [HttpPost("[action]")]
        public bool PostLogin([FromBody]LoginCredentials loginCredentials)
        {
            bool result;

            result = loginSystem.Login(loginCredentials, database);
            return result;
        }

        [HttpGet("[action]")]
        public BoolContainer CheckLoginStatus()
        {
            BoolContainer loginStatus = new BoolContainer();
            loginStatus.result = loginSystem.loginStatus;
            return loginStatus;
        }

        #endregion LoginSystem


































        //// GET: api/Login
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Login/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Login
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Login/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
