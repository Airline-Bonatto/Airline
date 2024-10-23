using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airline.DAL;
using Airline.Database;
using AirlineAPI.Dataviews;
using AirlineAPIV2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("aircraft")]
    public class AircraftController : ControllerBase
    {
        [HttpPost()]
        public IResult Create([FromBody] Aircraft aircraft)
        {
            var context = new AirlineContext();
            var dal = new DAL<Aircraft>(context);
            dal.Register(aircraft);

            return Results.Created();
            
        }

        [HttpGet("list")]
        public IResult List()
        {
            var context = new AirlineContext();
            var dal = new DAL<Aircraft>(context);

            return Results.Ok(dal.List());
        }


        [HttpGet("{id}")]
        public IResult Detail(int id)
        {
            var context = new AirlineContext();
            var dal = new DAL<Aircraft>(context);

            var aircraft = dal.GetById(id);

            if(aircraft == null)
            {
                return Results.NotFound("Aircraft not found!");
            }

            return Results.Ok(new AircraftDetailView(aircraft));
        }

    }


}