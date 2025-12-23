using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Airline.DTO;
using Airline.Repositories.Interfaces;
using Airline.RequestBodies;

namespace Airline.Validators.Route;

public class RouteAlreadyExistsValidation(
    IRouteRepository routeRepository,
    RouteInsertRequestBody data
) : IValidation
{
    private readonly IRouteRepository _routeRepository = routeRepository;
    private readonly RouteInsertRequestBody _data = data;


    public void Validate()
    {
        RouteListFiltersDTO filters = new()
        {
            From = _data.From,
            To = _data.To
        };

        RouteListDTO? route = _routeRepository.ListAsync(filters)
            .Result
            .FirstOrDefault();

        if(route is not null)
        {
            throw new ValidationException("Route already exists.");
        }
    }
}