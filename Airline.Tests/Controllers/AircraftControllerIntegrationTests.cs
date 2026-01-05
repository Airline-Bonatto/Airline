using System.Collections.Generic;
using System.Linq;

using Airline.DTO;
using Airline.Models;
using Airline.Repositories.Implementations;
using Airline.Repositories.Interfaces;

using Xunit;

namespace Airline.Tests.Controllers;


public class AircraftControllerIntegrationTests : IntegrationTestBase
{
    private readonly AircraftRepository _aircraftRepository;

    public AircraftControllerIntegrationTests()
    {
        _aircraftRepository = new AircraftRepository(Context);
    }

    [Fact]
    public void Create_ShouldInsertAircraftInDatabase()
    {
        // Arrange 
        AircraftCreateDTO aircraftCreateDto = new(
            Model: "Boeing 737",
            Capacity: 180,
            Range: 5000.0,
            AverageFuelConsumption: 2500.0
        );

        // Act 
        _aircraftRepository.Insert(aircraftCreateDto);

        // Assert 
        List<Aircraft> aircraftsInDb = Context.Aircrafts.ToList();
        Assert.Single(aircraftsInDb);

        Aircraft createdAircraft = aircraftsInDb.First();

        Assert.Equal("Boeing 737", createdAircraft.Model);
        Assert.Equal(180, createdAircraft.Capacity);
        Assert.Equal(5000.0, createdAircraft.Range);
        Assert.Equal(2500.0, createdAircraft.AverageFuelConsumption);

        Assert.True(createdAircraft.AircraftID > 0);
    }

    [Fact]
    public void Create_MultipleAircrafts_ShouldInsertAllInDatabase()
    {
        // Arrange 
        AircraftCreateDTO aircraft1 = new("Boeing 737", 180, 5000.0, 2500.0);
        AircraftCreateDTO aircraft2 = new("Airbus A320", 150, 6000.0, 2300.0);
        AircraftCreateDTO aircraft3 = new("Embraer E195", 120, 3700.0, 1800.0);

        // Act 
        _aircraftRepository.Insert(aircraft1);
        _aircraftRepository.Insert(aircraft2);
        _aircraftRepository.Insert(aircraft3);

        // Assert - Verificar se todos foram salvos
        List<Aircraft> aircraftsInDb = Context.Aircrafts.ToList();
        Assert.Equal(3, aircraftsInDb.Count);

        // Verifica se cada um tem ID único
        List<int> ids = aircraftsInDb.Select(a => a.AircraftID).ToList();
        Assert.Equal(3, ids.Distinct().Count());
    }

    [Fact]
    public void Create_ShouldBeRetrievableByGetAircraft()
    {
        // Arrange
        AircraftCreateDTO aircraftCreateDto = new("Boeing 787", 242, 14800.0, 5400.0);

        // Act 
        _aircraftRepository.Insert(aircraftCreateDto);
        Aircraft createdAircraft = Context.Aircrafts.First();
        Aircraft retrievedAircraft = _aircraftRepository.GetAircraft(createdAircraft.AircraftID);

        // Assert - Verifica se consegue buscar o aircraft criado
        Assert.NotNull(retrievedAircraft);
        Assert.Equal("Boeing 787", retrievedAircraft.Model);
        Assert.Equal(242, retrievedAircraft.Capacity);
    }
}
