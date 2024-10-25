using Airline.DAL;
using Airline.Database;
using AirlineAPI.Dataviews;
using AirlineAPI.DTO;
using AirlineAPIV2.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers
{
    [ApiController]
    [Route("aircraft")]
    public class AircraftController : ControllerBase
    {
        [HttpPost("create")]
        public IResult Create([FromServices] DAL<Aircraft> dal, [FromBody] AircraftCreateDTO createData)
        {
            dal.Register(new Aircraft(createData));

            return Results.Created();
            
        }

        [HttpGet("list")]
        public IResult List([FromServices] DAL<Aircraft> dal)
        {
            return Results.Ok(dal.List());
        }


        [HttpGet("{id}")]
        public IResult Detail([FromServices] DAL<Aircraft> dal, int id)
        {
            var aircraft = dal.GetById(id);

            if(aircraft == null)
            {
                return Results.NotFound("Aircraft not found!");
            }

            return Results.Ok(new AircraftDetailView(aircraft));
        }

        [HttpPost("update/{id}")]
        public IResult Update([FromServices] DAL<Aircraft> dal, [FromBody] AircraftUpdateDto updateData, int id)
        {
            var aircraft = dal.GetById(id);

            if(aircraft == null)
            {
                return Results.NotFound("Aircraft not found!");
            }

            aircraft.Update(updateData);
            dal.Update(aircraft);


            return Results.Ok();
        }

        [HttpDelete("{id}")]
        public IResult Remove([FromServices] DAL<Aircraft> dal,int id)
        {
            var aircraft = dal.GetById(id);

            if(aircraft == null)
            {
                return Results.NotFound("Aircraft not found!");
            }
            dal.Remove(aircraft);

            return Results.NoContent();
        }
    }


}