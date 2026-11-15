using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElectiveService.Services;
using System.Threading.Tasks;

namespace ElectiveService.Controllers
{
    [RoutePrefix("api/log")]
    public class LogController : ApiController
    {
        private readonly LogService myLogService = new LogService();

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            //returns status that the request was successful
            return Ok(myLogService.GetLog());
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] MovieRequest request)
        {
            // Validate the request
            if (request == null || string.IsNullOrWhiteSpace(request.Title) || string.IsNullOrWhiteSpace(request.Year))
            {
                return BadRequest("Movie title and year are required.");
            }

            // Log the movie details
            await myLogService.LogMovie(
               request.Title,
               request.Year,
               request.UserRating);

            return Ok("Movie added to your log.");
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete()
        {
            myLogService.ClearLog();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
    // MovieRequest class to represent the request body for logging a movie
    public class MovieRequest
    { 
        public string Title { get; set; }
        public string Year { get; set; }
        public decimal UserRating { get; set; }
    }
}
