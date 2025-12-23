using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Implementations;
using Airline.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Xunit;

using Route = Airline.Models.Route;

namespace Airline.Tests;

public class FlightCreateServiceTests : IntegrationTestBase
{
    private readonly FlightRepository _flightRepository;
    private readonly AircraftRepository _aircraftRepository;
    private readonly RouteRepository _routeRepository;
    private readonly FlightCreateService _service;

    public FlightCreateServiceTests()
    {
        _flightRepository = new FlightRepository(Context);
        _aircraftRepository = new AircraftRepository(Context);
        _routeRepository = new RouteRepository(Context);
        _service = new FlightCreateService(
            _flightRepository,
            _aircraftRepository,
            _routeRepository
        );
    }

    [Fact]
    public async Task Create_ShouldThrowEntityNotFoundException_WhenAircraftNotFound()
    {
        // Arrange
        FlightCreateDTO dto = new(
            RouteId: 1,
            AircraftId: 999,
            Departure: DateTimeOffset.UtcNow,
            Arrival: DateTimeOffset.UtcNow.AddHours(2)
        );

        // Act & Assert
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _service.Create(dto)
        );
        Assert.Equal("Aircraft not found", exception.Message);
    }

    [Fact]
    public async Task Create_ShouldThrowEntityNotFoundException_WhenRouteNotFound()
    {
        // Arrange
        Aircraft aircraft = new()
        {
            Model = "Boeing 737",
            Capacity = 180,
            AverageFuelConsumption = 2500,
            Range = 5000
        };
        Context.Aircrafts.Add(aircraft);
        await Context.SaveChangesAsync();

        FlightCreateDTO dto = new(
            RouteId: 999,
            AircraftId: aircraft.AircraftID,
            Departure: DateTimeOffset.UtcNow,
            Arrival: DateTimeOffset.UtcNow.AddHours(2)
        );

        // Act & Assert
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _service.Create(dto)
        );
        Assert.Equal("Route not found", exception.Message);
    }

    [Fact]
    public async Task Create_ShouldCreateFlightSuccessfully_WhenDataIsValid()
    {
        // Arrange
        Aircraft aircraft = new()
        {
            Model = "Boeing 737",
            Capacity = 180,
            AverageFuelConsumption = 2500,
            Range = 5000
        };
        Context.Aircrafts.Add(aircraft);

        Route route = new()
        {
            From = "São Paulo",
            To = "Rio de Janeiro",
            Distance = 400
        };
        Context.Routes.Add(route);
        await Context.SaveChangesAsync();

        FlightCreateDTO dto = new(
            RouteId: route.RouteID,
            AircraftId: aircraft.AircraftID,
            Departure: new DateTimeOffset(2025, 12, 25, 10, 0, 0, TimeSpan.Zero),
            Arrival: new DateTimeOffset(2025, 12, 25, 12, 0, 0, TimeSpan.Zero)
        );

        // Act
        int result = await _service.Create(dto);

        // Assert
        Assert.True(result > 0);

        List<Flight> flightsInDb = await Context.Flights
            .Include(f => f.Aircraft)
            .Include(f => f.Route)
            .ToListAsync();

        Assert.Single(flightsInDb);

        Flight createdFlight = flightsInDb.First();
        Assert.Equal(aircraft.AircraftID, createdFlight.Aircraft.AircraftID);
        Assert.Equal(route.RouteID, createdFlight.Route.RouteID);
        Assert.Equal(dto.Departure.ToUniversalTime(), createdFlight.Departure);
        Assert.Equal(dto.Arrival.ToUniversalTime(), createdFlight.Arrival);
        Assert.Equal(result, createdFlight.FlightId);
    }

    [Fact]
    public async Task Create_ShouldConvertDatesToUtc_WhenCreatingFlight()
    {
        // Arrange
        Aircraft aircraft = new()
        {
            Model = "Airbus A320",
            Capacity = 150,
            AverageFuelConsumption = 2200,
            Range = 6000
        };
        Context.Aircrafts.Add(aircraft);

        Route route = new()
        {
            From = "Brasília",
            To = "Salvador",
            Distance = 1200
        };
        Context.Routes.Add(route);
        await Context.SaveChangesAsync();

        // Create dates with non-UTC offset
        DateTimeOffset departureLocal = new(2025, 12, 25, 14, 30, 0, TimeSpan.FromHours(-3));
        DateTimeOffset arrivalLocal = new(2025, 12, 25, 17, 0, 0, TimeSpan.FromHours(-3));

        FlightCreateDTO dto = new(
            RouteId: route.RouteID,
            AircraftId: aircraft.AircraftID,
            Departure: departureLocal,
            Arrival: arrivalLocal
        );

        // Act
        await _service.Create(dto);

        // Assert
        Flight createdFlight = await Context.Flights.FirstAsync();
        Assert.NotNull(createdFlight);
        Assert.Equal(departureLocal.ToUniversalTime(), createdFlight.Departure);
        Assert.Equal(arrivalLocal.ToUniversalTime(), createdFlight.Arrival);
    }

    [Fact]
    public async Task Create_MultipleFlights_ShouldInsertAllInDatabase()
    {
        // Arrange
        Aircraft aircraft1 = new()
        {
            Model = "Boeing 737",
            Capacity = 180,
            AverageFuelConsumption = 2500,
            Range = 5000
        };

        Aircraft aircraft2 = new()
        {
            Model = "Airbus A320",
            Capacity = 150,
            AverageFuelConsumption = 2200,
            Range = 6000
        };

        Route route1 = new()
        {
            From = "São Paulo",
            To = "Rio de Janeiro",
            Distance = 400
        };

        Route route2 = new()
        {
            From = "Brasília",
            To = "Salvador",
            Distance = 1200
        };

        Context.Aircrafts.AddRange(aircraft1, aircraft2);
        Context.Routes.AddRange(route1, route2);
        await Context.SaveChangesAsync();

        FlightCreateDTO dto1 = new(
            RouteId: route1.RouteID,
            AircraftId: aircraft1.AircraftID,
            Departure: new DateTimeOffset(2025, 12, 25, 10, 0, 0, TimeSpan.Zero),
            Arrival: new DateTimeOffset(2025, 12, 25, 12, 0, 0, TimeSpan.Zero)
        );

        FlightCreateDTO dto2 = new(
            RouteId: route2.RouteID,
            AircraftId: aircraft2.AircraftID,
            Departure: new DateTimeOffset(2025, 12, 26, 14, 0, 0, TimeSpan.Zero),
            Arrival: new DateTimeOffset(2025, 12, 26, 16, 30, 0, TimeSpan.Zero)
        );

        // Act
        int id1 = await _service.Create(dto1);
        int id2 = await _service.Create(dto2);

        // Assert
        List<Flight> flightsInDb = await Context.Flights.ToListAsync();
        Assert.Equal(2, flightsInDb.Count);

        Assert.True(id1 > 0);
        Assert.True(id2 > 0);
        Assert.NotEqual(id1, id2);
    }

    [Fact]
    public async Task Create_ShouldBeRetrievableByGetByIdAsync()
    {
        // Arrange
        Aircraft aircraft = new()
        {
            Model = "Boeing 777",
            Capacity = 300,
            AverageFuelConsumption = 3000,
            Range = 10000
        };
        Context.Aircrafts.Add(aircraft);

        Route route = new()
        {
            From = "Porto Alegre",
            To = "Curitiba",
            Distance = 560
        };
        Context.Routes.Add(route);
        await Context.SaveChangesAsync();

        FlightCreateDTO dto = new(
            RouteId: route.RouteID,
            AircraftId: aircraft.AircraftID,
            Departure: new DateTimeOffset(2025, 12, 27, 8, 0, 0, TimeSpan.Zero),
            Arrival: new DateTimeOffset(2025, 12, 27, 9, 30, 0, TimeSpan.Zero)
        );

        // Act
        int flightId = await _service.Create(dto);
        Flight retrievedFlight = await _flightRepository.GetByIdAsync(flightId);

        // Assert
        Assert.NotNull(retrievedFlight);
        Assert.Equal(flightId, retrievedFlight.FlightId);
        Assert.Equal(dto.Departure.ToUniversalTime(), retrievedFlight.Departure);
        Assert.Equal(dto.Arrival.ToUniversalTime(), retrievedFlight.Arrival);
    }
}

