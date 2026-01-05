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

public class TicketPurchaseServiceTests : IntegrationTestBase
{
    private readonly TicketPurchaseService _ticketPurchaseService;
    private readonly TicketRepository _ticketRepository;
    private readonly AirlineUserRepository _airlineUserRepository;
    private readonly SeatRepository _seatRepository;
    private readonly FlightRepository _flightRepository;

    public TicketPurchaseServiceTests()
    {
        _ticketRepository = new TicketRepository(Context);
        _airlineUserRepository = new AirlineUserRepository(Context);
        _seatRepository = new SeatRepository(Context);
        _flightRepository = new FlightRepository(Context);
        _ticketPurchaseService = new TicketPurchaseService(
            _ticketRepository,
            _airlineUserRepository,
            _seatRepository
        );
    }

    private async Task<AirlineUser> CreateTestUserAsync(string document = "12345678900")
    {
        AirlineUser user = new()
        {
            Email = "test@example.com",
            Password = "password123",
            Document = document,
            Name = "John",
            LastName = "Doe"
        };
        Context.AirlineUsers.Add(user);
        await Context.SaveChangesAsync();
        return user;
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

    private async Task<Seat> CreateTestSeatAsync(int flightId, bool isAvailable = true)
    {
        Flight flight = await Context.Flights.FindAsync(flightId);
        SeatCreateDTO seatData = new()
        {
            SeatNumber = 1,
            Row = "A",
            IsAvailable = isAvailable,
            SeatClass = SeatClassEnum.Economic,
            AircraftAverageFuelConsumption = 2.5,
            Distance = 430.5,
            AircraftCapacity = 180
        };

        Seat seat = Seat.Create(seatData);
        seat.FlightId = flightId;
        seat.Flight = flight!;
        Context.Seats.Add(seat);
        await Context.SaveChangesAsync();
        return seat;
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldCreateTicketSuccessfully_WhenDataIsValid()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();
        Flight flight = await CreateTestFlightAsync();
        Seat seat = await CreateTestSeatAsync(flight.FlightId);

        TicketPurchaseRequestDTO request = new()
        {
            SeatId = seat.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = "98765432100"
        };

        // Act
        int ticketId = await _ticketPurchaseService.PurchaseTicketAsync(request);

        // Assert
        Assert.True(ticketId > 0);
        Ticket ticket = await Context.Tickets.FindAsync(ticketId);
        Assert.NotNull(ticket);
        Assert.Equal(seat.SeatId, ticket.SeatId);
        Assert.Equal(user.AirlineUserId, ticket.AirlineUserId);
        Assert.Equal("98765432100", ticket.OwnerDocument);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldSetSeatAsUnavailable_WhenTicketIsPurchased()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();
        Flight flight = await CreateTestFlightAsync();
        Seat seat = await CreateTestSeatAsync(flight.FlightId, isAvailable: true);

        TicketPurchaseRequestDTO request = new()
        {
            SeatId = seat.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = "98765432100"
        };

        // Act
        await _ticketPurchaseService.PurchaseTicketAsync(request);

        // Assert
        Seat updatedSeat = await Context.Seats.FindAsync(seat.SeatId);
        Assert.NotNull(updatedSeat);
        Assert.False(updatedSeat.IsAvailable);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldThrowEntityNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        Flight flight = await CreateTestFlightAsync();
        Seat seat = await CreateTestSeatAsync(flight.FlightId);

        TicketPurchaseRequestDTO request = new()
        {
            SeatId = seat.SeatId,
            AirlineUserId = 999, // Non-existent user
            OwnerDocument = "98765432100"
        };

        // Act & Assert
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _ticketPurchaseService.PurchaseTicketAsync(request)
        );
        Assert.Equal("User not found.", exception.Message);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldThrowEntityNotFoundException_WhenSeatDoesNotExist()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();

        TicketPurchaseRequestDTO request = new()
        {
            SeatId = 999, // Non-existent seat
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = "98765432100"
        };

        // Act & Assert
        EntityNotFoundException exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _ticketPurchaseService.PurchaseTicketAsync(request)
        );
        Assert.Equal("Seat not found.", exception.Message);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldThrowTicketPurchaseException_WhenUserAlreadyHasTicketForFlight()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();
        Flight flight = await CreateTestFlightAsync();
        Seat seat1 = await CreateTestSeatAsync(flight.FlightId);
        Seat seat2 = await CreateTestSeatAsync(flight.FlightId);

        string ownerDocument = "98765432100";

        // First purchase
        TicketPurchaseRequestDTO firstRequest = new()
        {
            SeatId = seat1.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = ownerDocument
        };
        await _ticketPurchaseService.PurchaseTicketAsync(firstRequest);

        // Second purchase attempt for same flight with same owner document
        TicketPurchaseRequestDTO secondRequest = new()
        {
            SeatId = seat2.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = ownerDocument
        };

        // Act & Assert
        TicketPurchaseException exception = await Assert.ThrowsAsync<TicketPurchaseException>(
            () => _ticketPurchaseService.PurchaseTicketAsync(secondRequest)
        );
        Assert.Equal("This user already has a ticket for this flight.", exception.Message);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldAllowSameUserToBuyTicketsForDifferentFlights()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();
        Flight flight1 = await CreateTestFlightAsync();
        Flight flight2 = await CreateTestFlightAsync();
        Seat seat1 = await CreateTestSeatAsync(flight1.FlightId);
        Seat seat2 = await CreateTestSeatAsync(flight2.FlightId);

        string ownerDocument = "98765432100";

        TicketPurchaseRequestDTO request1 = new()
        {
            SeatId = seat1.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = ownerDocument
        };

        TicketPurchaseRequestDTO request2 = new()
        {
            SeatId = seat2.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = ownerDocument
        };

        // Act
        int ticketId1 = await _ticketPurchaseService.PurchaseTicketAsync(request1);
        int ticketId2 = await _ticketPurchaseService.PurchaseTicketAsync(request2);

        // Assert
        Assert.True(ticketId1 > 0);
        Assert.True(ticketId2 > 0);
        Assert.NotEqual(ticketId1, ticketId2);

        List<Ticket> tickets = await Context.Tickets
            .Where(t => t.OwnerDocument == ownerDocument)
            .ToListAsync();
        Assert.Equal(2, tickets.Count);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldAllowDifferentUsersToBuyTicketsForSameFlight()
    {
        // Arrange
        AirlineUser user1 = await CreateTestUserAsync("11111111111");
        AirlineUser user2 = await CreateTestUserAsync("22222222222");
        Flight flight = await CreateTestFlightAsync();
        Seat seat1 = await CreateTestSeatAsync(flight.FlightId);
        Seat seat2 = await CreateTestSeatAsync(flight.FlightId);

        TicketPurchaseRequestDTO request1 = new()
        {
            SeatId = seat1.SeatId,
            AirlineUserId = user1.AirlineUserId,
            OwnerDocument = "11111111111"
        };

        TicketPurchaseRequestDTO request2 = new()
        {
            SeatId = seat2.SeatId,
            AirlineUserId = user2.AirlineUserId,
            OwnerDocument = "22222222222"
        };

        // Act
        int ticketId1 = await _ticketPurchaseService.PurchaseTicketAsync(request1);
        int ticketId2 = await _ticketPurchaseService.PurchaseTicketAsync(request2);

        // Assert
        Assert.True(ticketId1 > 0);
        Assert.True(ticketId2 > 0);
        Assert.NotEqual(ticketId1, ticketId2);

        List<Ticket> tickets = await Context.Tickets
            .Where(t => t.Seat.FlightId == flight.FlightId)
            .ToListAsync();
        Assert.Equal(2, tickets.Count);
    }

    [Fact]
    public async Task PurchaseTicketAsync_ShouldPersistTicketInDatabase()
    {
        // Arrange
        AirlineUser user = await CreateTestUserAsync();
        Flight flight = await CreateTestFlightAsync();
        Seat seat = await CreateTestSeatAsync(flight.FlightId);

        TicketPurchaseRequestDTO request = new()
        {
            SeatId = seat.SeatId,
            AirlineUserId = user.AirlineUserId,
            OwnerDocument = "98765432100"
        };

        // Act
        int ticketId = await _ticketPurchaseService.PurchaseTicketAsync(request);

        // Assert - Verify ticket exists in database
        Ticket ticketInDb = await Context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        Assert.NotNull(ticketInDb);
        Assert.Equal(request.SeatId, ticketInDb.SeatId);
        Assert.Equal(request.AirlineUserId, ticketInDb.AirlineUserId);
        Assert.Equal(request.OwnerDocument, ticketInDb.OwnerDocument);
    }
}
