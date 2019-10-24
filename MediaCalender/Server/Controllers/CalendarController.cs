using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaCalender.Shared.Containers;
using MediaCalender.Shared.ContentTypes;

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

        [HttpPost("[action]")]
        public async Task<ResultContainer> AddFolSeries([FromBody]StringContainer stringContainer)
        {
            ResultContainer result;
            result = await Program.Classes.database.AddFolSeries(stringContainer.str);

            return result;
        }

        [HttpPost("[action]")]
        public async Task<List<Episode>> GetAllEpisodes([FromBody]StringContainer stringContainer)
        {
            List<Episode> episodeList;
            episodeList = await Program.Classes.database.GetAllEpisodes();

            return episodeList;
        }
    }
}