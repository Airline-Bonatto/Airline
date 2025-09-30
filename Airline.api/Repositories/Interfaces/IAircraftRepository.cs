
using Airline.Dataviews;
using Airline.DTO;

namespace Airline.Repositories.Interfaces;

public interface IAircraftRepository
{
    public IEnumerable<AircraftListDataView> ListAircrafts();
    public AircraftDetailView? GetAircraft(int aircraftId);
    public void Insert(AircraftCreateDTO createData);
    public void Update(AircraftUpdateDTO updateData);
}
