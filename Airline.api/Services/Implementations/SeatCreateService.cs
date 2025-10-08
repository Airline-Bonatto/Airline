using Airline.DTO;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.Services.Interfaces;

namespace Airline.Services.Implementations;

public class SeatCreateService(
    IFlightRepository flightRepository,
    ISeatRepository seatRepository
) : ISeatCreateService
{
    private readonly IFlightRepository _flightRepository = flightRepository;
    private readonly ISeatRepository _seatRepository = seatRepository;
    public async Task CreateAsync(SeatCreateRequestDTO data)
    {
        Flight? flight = await _flightRepository.GetByIdAsync(data.FlightId);
        if(flight is null)
            throw new EntityNotFoundException("Flight not found");

        AddSeats(flight, data.QuantityEconomic, Enuns.SeatClassEnum.Economic, 6);
        AddSeats(flight, data.QuantityExecutive, Enuns.SeatClassEnum.Executive, 6);
        AddSeats(flight, data.QuantityFirstClass, Enuns.SeatClassEnum.FirstClass, 4);

        await _seatRepository.AddRangeAsync(flight.Seats);

    }

    private static void AddSeats(Flight flight, int quantity, Enuns.SeatClassEnum seatClass, int seatsPerRow)
    {
        for(int i = 0; i < quantity; i++)
        {
            SeatCreateDTO seatData = new()
            {
                SeatNumber = (i / seatsPerRow) + 1,
                Row = ((char)('A' + (i % seatsPerRow))).ToString(),
                IsAvailable = true,
                SeatClass = seatClass,
                AircraftAverageFuelConsumption = flight.Aircraft.AverageFuelConsumption,
                AircraftCapacity = flight.Aircraft.Capacity,
                Distance = flight.Route.Distance
            };
            Seat seat = Seat.Create(seatData);
            flight.Seats.Add(seat);
        }
    }
}