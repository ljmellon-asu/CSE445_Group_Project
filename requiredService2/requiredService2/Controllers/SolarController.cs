using requiredService2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace requiredService2.Controllers
{
    [RoutePrefix("api/solar")]  
    public class SolarController : ApiController
    {
        private readonly SolarService mySolarService = new SolarService();

        [HttpGet]
        [Route("intensity")]

        public async Task<decimal?> GetSolarIntensity(decimal latitude, decimal longitude)
        {
           return await mySolarService.SolarIntensity(latitude, longitude);
          
        }
    }
}
