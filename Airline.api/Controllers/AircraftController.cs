using AirlineAPI.Airline.api.Dataviews;
using AirlineAPI.Airline.api.DTO;
using AirlineAPI.Airline.api.Models;
using AirlineAPI.Airline.api.Repositories.Interfaces;
using AirlineAPI.Airline.api.RequestBodies;

using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Airline.api.Controllers;

[ApiController]
[Route("aircraft")]
public class AircraftController(IAircraftRepository aircraftRepository) : ControllerBase
{

    private readonly IAircraftRepository _aircraftRepository = aircraftRepository;

    [HttpPost("create")]
    public IResult Create([FromBody] AircraftCreateDTO createData)
    {
        _aircraftRepository.Insert(createData);

        return Results.Created();

    }

    [HttpGet("list")]
    public IResult List(
        [FromQuery] int page = 1,
        [FromQuery] int perPage = 10)
    {
        return Results.Ok(_aircraftRepository.ListAircrafts());
    }


    [HttpGet("{aircraftId}")]
    public IResult Detail(int aircraftId)
    {
        AircraftDetailView? aircraft = _aircraftRepository.GetAircraft(aircraftId);

        if(aircraft == null)
        {
            return Results.NotFound(new { Message = "Aircraft not found!" });
        }

        return Results.Ok(_aircraftRepository.GetAircraft(aircraftId));
    }

    [HttpPatch("update/{id}")]
    public IResult Update([FromBody] AircraftUpdateRequestBody updateData, int id)
    {

        AircraftUpdateDTO updateDto = new(updateData, id);
        _aircraftRepository.Update(updateDto);
        return Results.Ok();
    }

    [HttpDelete("{aircraftId}")]
    public IResult Remove(int aircraftId)
    {
        AircraftUpdateDTO updateDto = new(aircraftId, null, null, null, DateTime.Now);
        _aircraftRepository.Update(updateDto);
        return Results.Ok();
    }
}