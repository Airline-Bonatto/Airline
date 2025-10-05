using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;

using Microsoft.AspNetCore.Mvc;

namespace Airline.Controllers;

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
        Aircraft? aircraft = _aircraftRepository.GetAircraft(aircraftId);

        if(aircraft == null)
        {
            return Results.NotFound(new { Message = "Aircraft not found!" });
        }

        return Results.Ok(_aircraftRepository.GetAircraft(aircraftId));
    }

    [HttpPatch("update/{id}")]
    public IResult Update([FromBody] AircraftUpdateRequestBody updateData, int id)
    {
        try
        {
            AircraftUpdateDTO updateDto = new(updateData, id);
            _aircraftRepository.Update(updateDto);
            return Results.Ok();
        }
        catch(EntityNotFoundException e)
        {
            return Results.NotFound(new { Message = e.Message });
        }


    }

    [HttpDelete("{aircraftId}")]
    public async Task<IResult> RemoveAsync(int aircraftId)
    {
        try
        {
            await _aircraftRepository.DeleteAsync(aircraftId);
            return Results.Ok();
        }
        catch(EntityNotFoundException e)
        {
            return Results.NotFound(new { Message = e.Message });
        }
    }
}