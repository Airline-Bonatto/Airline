using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airline.DAL;
using Airline.Database;
using AirlineAPIV2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("aircraft")]
    public class AircraftController : ControllerBase
    {
        [HttpPost(Name = "CreateAircraft")]
        public IResult Create([FromBody] Aircraft aircraft)
        {
            var context = new AirlineContext();
            var dal = new DAL<Aircraft>(context);
            dal.Register(aircraft);

            return Results.Created();
            
        }

        [HttpGet(Name = "ListAircrafts")]
        public IResult List()
        {
            var context = new AirlineContext();
            var dal = new DAL<Aircraft>(context);

            return Results.Ok(dal.List());
        }
    }


}