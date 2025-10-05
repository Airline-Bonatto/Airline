
using Airline.DTO;
using Airline.Models;

namespace Airline.Repositories.Interfaces;

public interface IAircraftRepository
{
    public IEnumerable<Aircraft> ListAircrafts();
    public Aircraft? GetAircraft(int aircraftId);
    public void Insert(AircraftCreateDTO createData);
    public void Update(AircraftUpdateDTO updateData);
    public Task DeleteAsync(int aircraftId);
}
