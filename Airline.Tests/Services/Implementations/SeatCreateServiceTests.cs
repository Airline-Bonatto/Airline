using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Airline.DTO;
using Airline.Enuns;
using Airline.Exceptions;
using Airline.Models;
using Airline.Repositories.Interfaces;
using Airline.Services.Implementations;

using Moq;

using Xunit;

namespace Airline.Tests.Services.Implementations
{
    public class SeatCreateServiceTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<ISeatRepository> _seatRepositoryMock;
        private readonly SeatCreateService _service;

        public SeatCreateServiceTests()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _seatRepositoryMock = new Mock<ISeatRepository>();
            _service = new SeatCreateService(_flightRepositoryMock.Object, _seatRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrow_WhenFlightNotFound()
        {
            var dto = new SeatCreateRequestDTO { FlightId = 1 };
            _flightRepositoryMock.Setup(r => r.GetByIdAsync(dto.FlightId)).ReturnsAsync((Flight)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddSeatsAndCallRepository()
        {
            var flight = new Flight
            {
                Aircraft = new Aircraft { AverageFuelConsumption = 10, Capacity = 100 },
                Route = new Route { Distance = 500 },
                Seats = new List<Seat>()
            };
            var dto = new SeatCreateRequestDTO
            {
                FlightId = 1,
                QuantityEconomic = 3,
                QuantityExecutive = 2,
                QuantityFirstClass = 1
            };
            _flightRepositoryMock.Setup(r => r.GetByIdAsync(dto.FlightId)).ReturnsAsync(flight);
            _seatRepositoryMock.Setup(r => r.AddRangeAsync(It.IsAny<List<Seat>>())).Returns(Task.CompletedTask);

            await _service.CreateAsync(dto);


            List<Seat> economicSeats = flight.Seats.Where(s => s.SeatClass == SeatClassEnum.Economic).ToList();
            List<Seat> executiveSeats = flight.Seats.Where(s => s.SeatClass == SeatClassEnum.Executive).ToList();
            List<Seat> firstClassSeats = flight.Seats.Where(s => s.SeatClass == SeatClassEnum.FirstClass).ToList();


            Assert.Equal(3, economicSeats.Count);
            Assert.Equal(2, executiveSeats.Count);
            Assert.Single(firstClassSeats);
            Assert.Equal(6, flight.Seats.Count);
            Assert.All(flight.Seats, s => Assert.True(s.IsAvailable));

            _seatRepositoryMock.Verify(r => r.AddRangeAsync(flight.Seats), Times.Once);
        }
    }
}
