using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Airline.DTO;
using Airline.Enuns;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Implementations;
using Airline.Services.Implementations;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace Airline.Tests.Services.Implementations;

public class SeatCreateServiceIntegrationTests : IntegrationTestBase
{
    private readonly SeatCreateService _seatCreateService;
    private readonly FlightRepository _flightRepository;
    private readonly SeatRepository _seatRepository;

    public SeatCreateServiceIntegrationTests()
    {
        _flightRepository = new FlightRepository(Context);
        _seatRepository = new SeatRepository(Context);
        _seatCreateService = new SeatCreateService(_flightRepository, _seatRepository);
    }

    private async Task<Flight> CreateTestFlightAsync()
    {
        Route route = new(new RouteMergeDTO(
            From: "São Paulo",
            To: "Rio de Janeiro",
            Distance: 430.5
        ));
        Context.Routes.Add(route);

        Aircraft aircraft = new(new AircraftCreateDTO(
            Model: "Boeing 737",
            Capacity: 180,
            Range: 5000.0,
            AverageFuelConsumption: 2.5
        ));
        Context.Aircrafts.Add(aircraft);

        await Context.SaveChangesAsync();

        Flight flight = new(new FlightCreateDTO(
            RouteId: route.RouteID,
            AircraftId: aircraft.AircraftID,
            Departure: DateTimeOffset.Now.AddDays(1),
            Arrival: DateTimeOffset.Now.AddDays(1).AddHours(2)
        ))
        {
            Route = route,
            Aircraft = aircraft
        };
        Context.Flights.Add(flight);
        await Context.SaveChangesAsync();

        return flight;
    }

    [Fact]
    public async Task CreateAsync_ShouldInsertSeatsInDatabase()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 12,
            QuantityExecutive = 6,
            QuantityFirstClass = 4
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seatsInDb = await Context.Seats.Where(s => s.FlightId == flight.FlightId).ToListAsync();


        List<Seat> economicSeats = seatsInDb
            .Where(s => s.FlightId == flight.FlightId && s.SeatClass == SeatClassEnum.Economic)
            .ToList();

        List<Seat> executiveSeats = seatsInDb
            .Where(s => s.FlightId == flight.FlightId && s.SeatClass == SeatClassEnum.Executive)
            .ToList();

        List<Seat> firstClassSeats = seatsInDb
            .Where(s => s.FlightId == flight.FlightId && s.SeatClass == SeatClassEnum.FirstClass)
            .ToList();

        Assert.Equal(22, seatsInDb.Count);
        Assert.Equal(12, economicSeats.Count);
        Assert.Equal(6, executiveSeats.Count);
        Assert.Equal(4, firstClassSeats.Count);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateSeatsWithCorrectSeatNumbers()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 12,
            QuantityExecutive = 0,
            QuantityFirstClass = 0
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats
            .Where(s => s.FlightId == flight.FlightId)
            .OrderBy(s => s.SeatNumber)
            .ThenBy(s => s.Row)
            .ToListAsync();

        Assert.Equal(1, seats[0].SeatNumber);
        Assert.Equal("A", seats[0].Row);
        Assert.Equal(1, seats[5].SeatNumber);
        Assert.Equal("F", seats[5].Row);
        Assert.Equal(2, seats[6].SeatNumber);
        Assert.Equal("A", seats[6].Row);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateSeatsWithCorrectRows()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 6,
            QuantityExecutive = 0,
            QuantityFirstClass = 0
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats
            .Where(s => s.FlightId == flight.FlightId)
            .OrderBy(s => s.Row)
            .ToListAsync();

        Assert.Equal("A", seats[0].Row);
        Assert.Equal("B", seats[1].Row);
        Assert.Equal("C", seats[2].Row);
        Assert.Equal("D", seats[3].Row);
        Assert.Equal("E", seats[4].Row);
        Assert.Equal("F", seats[5].Row);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateAllSeatsAsAvailable()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 6,
            QuantityExecutive = 6,
            QuantityFirstClass = 4
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats
            .Where(s => s.FlightId == flight.FlightId)
            .ToListAsync();

        Assert.All(seats, seat => Assert.True(seat.IsAvailable));
    }

    [Fact]
    public async Task CreateAsync_ShouldSetPriceForAllSeats()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 6,
            QuantityExecutive = 6,
            QuantityFirstClass = 4
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats
            .Where(s => s.FlightId == flight.FlightId)
            .ToListAsync();

        Assert.All(seats, seat => Assert.True(seat.Price > 0));
    }

    [Fact]
    public async Task CreateAsync_WithInvalidFlightId_ShouldThrowEntityNotFoundException()
    {
        // Arrange
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = 999,
            QuantityEconomic = 6,
            QuantityExecutive = 6,
            QuantityFirstClass = 4
        };

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(
            async () => await _seatCreateService.CreateAsync(seatData)
        );
    }

    [Fact]
    public async Task CreateAsync_WithZeroSeats_ShouldNotInsertAnySeats()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 0,
            QuantityExecutive = 0,
            QuantityFirstClass = 0
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seatsInDb = await Context.Seats.Where(s => s.FlightId == flight.FlightId).ToListAsync();
        Assert.Empty(seatsInDb);
    }

    [Fact]
    public async Task CreateAsync_ShouldLinkSeatsToCorrectFlight()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 6,
            QuantityExecutive = 0,
            QuantityFirstClass = 0
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats
            .Include(s => s.Flight)
            .Where(s => s.FlightId == flight.FlightId)
            .ToListAsync();

        Assert.All(seats, seat =>
        {
            Assert.NotNull(seat.Flight);
            Assert.Equal(flight.FlightId, seat.Flight.FlightId);
        });
    }

    [Fact]
    public async Task CreateAsync_MultipleCalls_ShouldAddSeatsToSameFlight()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData1 = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 6,
            QuantityExecutive = 0,
            QuantityFirstClass = 0
        };
        SeatCreateRequestDTO seatData2 = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 0,
            QuantityExecutive = 6,
            QuantityFirstClass = 0
        };

        // Act
        await _seatCreateService.CreateAsync(seatData1);
        await _seatCreateService.CreateAsync(seatData2);

        // Assert
        List<Seat> seatsInDb = await Context.Seats.Where(s => s.FlightId == flight.FlightId).ToListAsync();
        Assert.Equal(12, seatsInDb.Count);

        int economicCount = seatsInDb.Count(s => s.SeatClass == SeatClassEnum.Economic);
        int executiveCount = seatsInDb.Count(s => s.SeatClass == SeatClassEnum.Executive);

        Assert.Equal(6, economicCount);
        Assert.Equal(6, executiveCount);
    }

    [Fact]
    public async Task CreateAsync_FirstClassSeats_ShouldHave4SeatsPerRow()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 0,
            QuantityExecutive = 0,
            QuantityFirstClass = 8
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> firstClassSeats = await Context.Seats
            .Where(s => s.FlightId == flight.FlightId && s.SeatClass == SeatClassEnum.FirstClass)
            .OrderBy(s => s.SeatNumber)
            .ThenBy(s => s.Row)
            .ToListAsync();

        Assert.Equal(1, firstClassSeats[0].SeatNumber);
        Assert.Equal("A", firstClassSeats[0].Row);
        Assert.Equal(1, firstClassSeats[3].SeatNumber);
        Assert.Equal("D", firstClassSeats[3].Row);
        Assert.Equal(2, firstClassSeats[4].SeatNumber);
        Assert.Equal("A", firstClassSeats[4].Row);
    }

    [Fact]
    public async Task CreateAsync_MixedClasses_ShouldCreateCorrectDistribution()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        SeatCreateRequestDTO seatData = new()
        {
            FlightId = flight.FlightId,
            QuantityEconomic = 30,
            QuantityExecutive = 12,
            QuantityFirstClass = 8
        };

        // Act
        await _seatCreateService.CreateAsync(seatData);

        // Assert
        List<Seat> seats = await Context.Seats.Where(s => s.FlightId == flight.FlightId).ToListAsync();

        int economicCount = seats.Count(s => s.SeatClass == SeatClassEnum.Economic);
        int executiveCount = seats.Count(s => s.SeatClass == SeatClassEnum.Executive);
        int firstClassCount = seats.Count(s => s.SeatClass == SeatClassEnum.FirstClass);

        Assert.Equal(30, economicCount);
        Assert.Equal(12, executiveCount);
        Assert.Equal(8, firstClassCount);
        Assert.Equal(50, seats.Count);
    }
}
