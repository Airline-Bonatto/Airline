
using AirlineAPI.Dataviews;
using AirlineAPI.DTO;

namespace AirlineAPI;

public interface IAircraftRepository
{
    public IEnumerable<AircraftListDataView> ListAircrafts();
    public AircraftDetailView? GetAircraft(int aircraftId);
    public void Insert(AircraftCreateDTO createData);

    public void Update(AircraftUpdateDTO updateData);
}
