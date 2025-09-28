
using AirlineAPI.Airline.api.Dataviews;
using AirlineAPI.Airline.api.DTO;

namespace AirlineAPI.Airline.api.Repositories.Interfaces;

public interface IAircraftRepository
{
    public IEnumerable<AircraftListDataView> ListAircrafts();
    public AircraftDetailView? GetAircraft(int aircraftId);
    public void Insert(AircraftCreateDTO createData);
    public void Update(AircraftUpdateDTO updateData);
}
