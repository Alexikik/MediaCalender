using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaCalender.Shared.Containers;

namespace MediaCalender.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {
        // Gets a string with some info on a specific movie (TEST)
        [HttpGet("[action]")]
        public StringContainer GetSpecificMovieInfo()
        {
            StringContainer answer = new StringContainer() { str = Program.Classes.database.PrintSpecificMovie(1) };
            return answer;
        }

        // Adds a specific movie by name and returns a bool indicating if the process was succesful
        [HttpPost("[action]")]
        public async Task<BoolContainer> PostMovieString([FromBody]StringContainer stringContainer)
        {
            BoolContainer answer;
            answer = await Program.Classes.database.AddMovieAsync(stringContainer.str);

            return answer;
        }
    }
}