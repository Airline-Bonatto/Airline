using System;
using System.Threading.Tasks;

using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Implementations;
using Airline.Services.Implementations;

using Microsoft.EntityFrameworkCore;

using Xunit;

using Route = Airline.Models.Route;

namespace Airline.Tests.Services.Implementations;

public class FlightDetailServiceTests : IntegrationTestBase
{
    private readonly FlightRepository _flightRepository;
    private readonly FlightDetailService _service;

    public FlightDetailServiceTests()
    {
        _flightRepository = new FlightRepository(Context);
        _service = new FlightDetailService(_flightRepository);
    }

    [Fact]
    public async Task Detail_ShouldThrowEntityNotFoundException_WhenFlightNotFound()
    {
        // Arrange
        int nonExistentFlightId = 999;

        // Act & Assert
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _service.Detail(nonExistentFlightId)
        );
        Assert.Equal("Flight not found", exception.Message);
    }

    [Fact]
    public async Task Detail_ShouldReturnFlightDetails_WhenFlightExists()
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

        DateTimeOffset departure = new(2025, 12, 25, 10, 0, 0, TimeSpan.Zero);
        DateTimeOffset arrival = new(2025, 12, 25, 12, 0, 0, TimeSpan.Zero);

        Flight flight = new()
        {
            Aircraft = aircraft,
            Route = route,
            Departure = departure,
            Arrival = arrival
        };
        Context.Flights.Add(flight);
        await Context.SaveChangesAsync();

        // Act
        FlightDetailDTO result = await _service.Detail(flight.FlightId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(flight.FlightId, result.FlightId);
        Assert.Equal(departure.UtcDateTime, result.Departure);
        Assert.Equal(arrival.UtcDateTime, result.Arrival);
        Assert.Equal(aircraft.Model, result.AircraftModel);
        Assert.Equal(route.From, result.From);
        Assert.Equal(route.To, result.To);
    }

    [Fact]
    public async Task Detail_ShouldReturnUtcDates_WhenFlightHasNonUtcDates()
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

        // Create dates with non-UTC offset (Brazilian time -3)
        DateTimeOffset departureLocal = new(2025, 12, 25, 14, 30, 0, TimeSpan.FromHours(-3));
        DateTimeOffset arrivalLocal = new(2025, 12, 25, 17, 0, 0, TimeSpan.FromHours(-3));

        Flight flight = new()
        {
            Aircraft = aircraft,
            Route = route,
            Departure = departureLocal,
            Arrival = arrivalLocal
        };
        Context.Flights.Add(flight);
        await Context.SaveChangesAsync();

        // Act
        FlightDetailDTO result = await _service.Detail(flight.FlightId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(departureLocal.UtcDateTime, result.Departure);
        Assert.Equal(arrivalLocal.UtcDateTime, result.Arrival);
        Assert.Equal(DateTimeKind.Utc, result.Departure.Kind);
        Assert.Equal(DateTimeKind.Utc, result.Arrival.Kind);
    }

    [Fact]
    public async Task Detail_ShouldIncludeRelatedEntities_WhenFlightExists()
    {
        // Arrange
        Aircraft aircraft = new()
        {
            Model = "Embraer E195",
            Capacity = 120,
            AverageFuelConsumption = 1800,
            Range = 4000
        };
        Context.Aircrafts.Add(aircraft);

        Route route = new()
        {
            From = "Porto Alegre",
            To = "Florianópolis",
            Distance = 450
        };
        Context.Routes.Add(route);

        Flight flight = new()
        {
            Aircraft = aircraft,
            Route = route,
            Departure = DateTimeOffset.UtcNow,
            Arrival = DateTimeOffset.UtcNow.AddHours(1)
        };
        Context.Flights.Add(flight);
        await Context.SaveChangesAsync();

        // Clear the context to ensure data is loaded from database
        Context.ChangeTracker.Clear();

        // Act
        FlightDetailDTO result = await _service.Detail(flight.FlightId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Embraer E195", result.AircraftModel);
        Assert.Equal("Porto Alegre", result.From);
        Assert.Equal("Florianópolis", result.To);
    }
}
